using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using HyPlayer.NeteaseApi.Extensions;
using Kengwang.Toolkit;

namespace HyPlayer.NeteaseApi.Bases.ApiContractBases;

public abstract class
    EApiContractBase<TRequest, TResponse, TError, TActualRequest> :
        ApiContractBase<TRequest, TResponse, TError, TActualRequest>
    where TActualRequest : EApiActualRequestBase
    where TError : ErrorResultBase
    where TRequest : RequestBase
    where TResponse : ResponseBase, new()
{
    public static readonly byte[] eapiKey = "e82ckenh8dichen8"u8.ToArray();
    public abstract string ApiPath { get; }

    public override async Task<HttpRequestMessage> GenerateRequestMessageAsync(ApiHandlerOption option, CancellationToken cancellationToken = default)
    {
        return await GenerateRequestMessageAsync(ActualRequest! ,option, cancellationToken);
    }

    public override Task<HttpRequestMessage> GenerateRequestMessageAsync<TActualRequestMessageModel>(TActualRequestMessageModel actualRequest, ApiHandlerOption option, CancellationToken cancellationToken = default)
    {
        var url = Regex.Replace(Url, @"\w*api", "eapi");
        if (option.DegradeHttp)
            url = url.Replace("https://", "http://");
        var requestMessage = new HttpRequestMessage(Method, url);
        if (Url.Contains("music.163.com"))
            requestMessage.Headers.Referrer = new Uri("https://music.163.com");
        if (!string.IsNullOrWhiteSpace(option.XRealIP))
            requestMessage.Headers.Add("X-Real-IP", option.XRealIP);
        requestMessage.Headers.UserAgent.Clear();
        requestMessage.Headers.TryAddWithoutValidation("User-Agent", UserAgentHelper.GetRandomUserAgent(UserAgent ?? option.UserAgent));

        var cookies = option.Cookies.ToDictionary(t => t.Key, t => t.Value);
        foreach (var keyValuePair in Cookies)
        {
            cookies[keyValuePair.Key] = keyValuePair.Value;
        }

        if (cookies.Count > 0)
            requestMessage.Headers.Add("Cookie", string.Join("; ", cookies.Select(c => $"{c.Key}={c.Value}")));

        var csrfToken = cookies.GetValueOrDefault("__csrf");
        var subDateTime = DateTime.Now.Subtract(new DateTime(1970, 1, 1));
        var dataHeader = new Dictionary<string, string?>()
                         {
                             { "osver", cookies.GetValueOrDefault("osver", string.Empty) },
                             {
                                 "deviceId", cookies.GetValueOrDefault("deviceId", string.Empty)
                             }, // encrypt.base64.encode(imei + '\t02:00:00:00:00:00\t5106025eb79a5247\t70ffbaac7')
                             { "appver", cookies.GetValueOrDefault("appver", "8.10.10") },
                             { "versioncode", cookies.GetValueOrDefault("versioncode", "140") },
                             { "mobilename", cookies.GetValueOrDefault("mobilename", string.Empty) },
                             {
                                 "buildver",
                                 cookies.GetValueOrDefault(
                                     "buildver", subDateTime.TotalSeconds.ToString(CultureInfo.InvariantCulture))
                             },
                             { "resolution", cookies.GetValueOrDefault("resolution", "1920x1080") },
                             { "__csrf", csrfToken },
                             { "os", cookies.GetValueOrDefault("os", "android") },
                             { "channel", cookies.GetValueOrDefault("channel", string.Empty) },
                             {
                                 "requestId",
                                 $"{subDateTime.TotalMilliseconds}_{Math.Floor(new Random().NextDouble() * 1000).ToString(CultureInfo.InvariantCulture).PadLeft(4, '0')}"
                             },
                         };
        if (!string.IsNullOrEmpty(cookies.GetValueOrDefault("MUSIC_U")))
            dataHeader["MUSIC_U"] = cookies.GetValueOrDefault("MUSIC_U");
        if (!string.IsNullOrEmpty(cookies.GetValueOrDefault("MUSIC_A")))
            dataHeader["MUSIC_A"] = cookies.GetValueOrDefault("MUSIC_A");
        var req = actualRequest as EApiActualRequestBase ?? new EApiActualRequestBase();
        req.Header = JsonSerializer.Serialize(dataHeader, option.JsonSerializerOptions);

        string json;
        if (req is TActualRequestMessageModel apiRequest)
            json = JsonSerializer.Serialize(apiRequest, option.JsonSerializerOptions);
        else
            json = JsonSerializer.Serialize(req, option.JsonSerializerOptions);
        var encryptMessage = $"nobody{ApiPath}use{json}md5forencrypt";
        var digest = encryptMessage.ToByteArrayUtf8().ComputeMd5().ToHexStringLower();
        var requestData = $"{ApiPath}-36cd479b6b5-{json}-36cd479b6b5-{digest}";

        using var aes = Aes.Create();
        aes.BlockSize = 128;
        aes.Key = eapiKey;
        aes.Mode = CipherMode.ECB;
        using var cryptoTransform = aes.CreateEncryptor();
        var reqDataBytes = requestData.ToByteArrayUtf8();
        var reqBytes = cryptoTransform.TransformFinalBlock(reqDataBytes, 0, reqDataBytes.Length);
        requestMessage.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
                                                           { { "params", reqBytes.ToHexStringUpper() } });
        return Task.FromResult(requestMessage);
    }

    public override async Task<Results<TResponse, ErrorResultBase>> ProcessResponseAsync(HttpResponseMessage response, ApiHandlerOption option,
                                                                                   CancellationToken cancellationToken = default)
    {
        return await ProcessResponseAsync<TResponse>(response, option, cancellationToken);
    }

    public override async Task<Results<TResponseModel, ErrorResultBase>> ProcessResponseAsync<TResponseModel>(
        HttpResponseMessage response, ApiHandlerOption option, CancellationToken cancellationToken = default)
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

        var buffer = await response.Content.ReadAsByteArrayAsync();
        if (buffer is null || buffer.Length == 0) return new ErrorResultBase(500, "返回体预读取错误");
        try
        {
            if (buffer[0] != 0x7B && buffer[1] != 0x22)
            {
                using var aes = Aes.Create();
                aes.BlockSize = 128;
                aes.Key = eapiKey;
                aes.Mode = CipherMode.ECB;
                using var decryptor = aes.CreateDecryptor();
                buffer = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
            }

            try
            {
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
                    return Results<TResponseModel, ErrorResultBase>
                           .CreateError(new ErrorResultBase(codedResponseBase.Code, "返回值不为 200")).WithValue(ret);
                return ret;
            }
            catch
            {
                // 防止加密后开头刚好是 {
                // return new ErrorResultBase(500, "JSON 解析错误");
                using var aes = Aes.Create();
                aes.BlockSize = 128;
                aes.Key = eapiKey;
                aes.Mode = CipherMode.ECB;
                using var decryptor = aes.CreateDecryptor();
                buffer = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
                var ret = JsonSerializer.Deserialize<TResponseModel>(Encoding.UTF8.GetString(buffer),
                                                                     option.JsonSerializerOptions);
                if (ret is null) return new ErrorResultBase(500, "返回 JSON 解析为空");
                if (ret is CodedResponseBase codedResponseBase && codedResponseBase.Code != 200)
                    return Results<TResponseModel, ErrorResultBase>
                           .CreateError(new ErrorResultBase(codedResponseBase.Code, "返回值不为 200")).WithValue(ret);
                return ret;
            }
        }
        catch when (buffer[0] == 123)
        {
            return new ErrorResultBase(462, "返回 JSON 解析为空");
        }
        catch (Exception e)
        {
            return new ExceptionedErrorBase(500, e.Message, e);
        }
    }

    class KeyValuePairKeyComparer : IEqualityComparer<KeyValuePair<string, string>>
    {
        public bool Equals(KeyValuePair<string, string> x, KeyValuePair<string, string> y)
        {
            return x.Key == y.Key;
        }

        public int GetHashCode(KeyValuePair<string, string> obj)
        {
            return obj.Key.GetHashCode();
        }
    }
}