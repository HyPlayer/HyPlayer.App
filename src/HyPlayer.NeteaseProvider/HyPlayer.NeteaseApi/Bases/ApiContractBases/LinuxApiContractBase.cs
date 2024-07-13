using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using HyPlayer.NeteaseApi.Extensions;
using Kengwang.Toolkit;

namespace HyPlayer.NeteaseApi.Bases.ApiContractBases;

public abstract class LinuxApiContractBase<TRequest, TResponse, TError, TActualRequest> :
    ApiContractBase<TRequest, TResponse, TError, TActualRequest>
    where TActualRequest : LinuxApiActualRequestBase
    where TError : ErrorResultBase
    where TRequest : RequestBase
    where TResponse : ResponseBase, new()

{
    private static readonly byte[] linuxapiKey = "rFgB&h#%2?^eDg:Q"u8.ToArray();

    public override async Task<HttpRequestMessage> GenerateRequestMessageAsync(ApiHandlerOption option, CancellationToken cancellationToken = default)
    {
        return await GenerateRequestMessageAsync(ActualRequest!, option, cancellationToken).ConfigureAwait(false);
    }

    public override Task<HttpRequestMessage> GenerateRequestMessageAsync<TActualRequestModel>(TActualRequestModel actualRequest,
        ApiHandlerOption option, CancellationToken cancellationToken = default)
    {
        var url = Url;
        if (option.DegradeHttp)
            url = url.Replace("https://", "http://");
        url = Regex.Replace(url, @"\w*api", "api");
        var requestMessage = new HttpRequestMessage(Method, "https://music.163.com/api/linux/forward");
        if (!string.IsNullOrWhiteSpace(option.XRealIP))
            requestMessage.Headers.Add("X-Real-IP", option.XRealIP);
        requestMessage.Headers.UserAgent.Clear();
        requestMessage.Headers.TryAddWithoutValidation("User-Agent",
                                                       "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36");
        if (Url.Contains("music.163.com"))
            requestMessage.Headers.Referrer = new Uri("https://music.163.com");
        var cookies = option.Cookies.ToDictionary(t => t.Key, t => t.Value);
        foreach (var keyValuePair in Cookies)
        {
            cookies[keyValuePair.Key] = keyValuePair.Value;
        }

        if (cookies.Count > 0)
            requestMessage.Headers.Add("Cookie", string.Join("; ", cookies.Select(c => $"{c.Key}={c.Value}")));
        
        var json = actualRequest is LinuxApiActualRequestBase la ? JsonSerializer.Serialize(la, option.JsonSerializerOptions) : string.Empty;
        var preData = JsonSerializer.Serialize(
            new Dictionary<string, string>()
            {
                { "method", "POST" },
                { "url", Regex.Replace(url, @"\w*api", "api") },
                { "params", json }
            }, option.JsonSerializerOptions);
        var data = new Dictionary<string, string>()
                   {
                       {
                           "eparams",
                           AesEncrypt(preData.ToByteArrayUtf8(), CipherMode.ECB, linuxapiKey, null).ToHexStringUpper()
                       }
                   };
        requestMessage.Content = new FormUrlEncodedContent(data);
        return Task.FromResult(requestMessage);
    }

    public override async Task<Results<TResponseModel, ErrorResultBase>> ProcessResponseAsync<TResponseModel>(HttpResponseMessage response, ApiHandlerOption option,
                                                                                                        CancellationToken cancellationToken = default)
    {
        if (!response.IsSuccessStatusCode)
            return new ErrorResultBase((int)response.StatusCode, $"请求返回 HTTP 代码: {response.StatusCode}");
        if (response.Headers.TryGetValues("Set-Cookie", out var rawSetCookies))
        {
            foreach (var rawSetCookie in rawSetCookies)
            {
                var arr1 = rawSetCookie.Split(';').ToList();
                var arr2 = arr1[0].TrimStart().Split('=');
                option.Cookies[arr2[0]] = arr2[1];
            }
        }

        var buffer = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        if (buffer is null || buffer.Length == 0) return new ErrorResultBase(500, "返回体预读取错误");

        var result = Encoding.UTF8.GetString(buffer);
        var ret = JsonSerializer.Deserialize<TResponseModel>(result, option.JsonSerializerOptions);
#if DEBUG
        if (ret is null)
            ret = new ();
        ret.OriginalResponse = result;
#endif
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (ret is null) return new ErrorResultBase(500, "返回 JSON 解析为空");
        if (ret is CodedResponseBase codedResponseBase && codedResponseBase.Code != 200)
            return Results<TResponseModel, ErrorResultBase>.CreateError(new ErrorResultBase(codedResponseBase.Code, "返回值不为 200")).WithValue(ret);
        return ret;
    }

    public override async Task<Results<TResponse, ErrorResultBase>> ProcessResponseAsync(
        HttpResponseMessage response, ApiHandlerOption option, CancellationToken cancellationToken = default)
    {
        return await ProcessResponseAsync<TResponse>(response, option, cancellationToken).ConfigureAwait(false);
    }


    private static byte[] AesEncrypt(byte[] buffer, CipherMode mode, byte[] key, byte[]? iv = null)
    {
        using var aes = Aes.Create();
        aes.BlockSize = 128;
        aes.Key = key;
        if (iv is not null)
            aes.IV = iv;
        aes.Mode = mode;
        using var cryptoTransform = aes.CreateEncryptor();
        return cryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length);
    }
}