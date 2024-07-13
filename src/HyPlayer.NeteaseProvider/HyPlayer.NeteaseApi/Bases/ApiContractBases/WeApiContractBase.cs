using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using HyPlayer.NeteaseApi.Extensions;
using Kengwang.Toolkit;

namespace HyPlayer.NeteaseApi.Bases.ApiContractBases;

public abstract class WeApiContractBase<TRequest, TResponse, TError, TActualRequest> :
    ApiContractBase<TRequest, TResponse, TError, TActualRequest>
    where TActualRequest : WeApiActualRequestBase
    where TError : ErrorResultBase
    where TRequest : RequestBase
    where TResponse : ResponseBase, new()
{
    private static readonly byte[] iv = "0102030405060708"u8.ToArray();

    private static readonly RSAParameters publicKey = ParsePublicKey(
        "-----BEGIN PUBLIC KEY-----\nMIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDgtQn2JZ34ZC28NWYpAUd98iZ37BUrX/aKzmFbt7clFSs6sXqHauqKWqdtLkF2KexO40H1YTX8z2lSgBBOAxLsvaklV8k4cBFK9snQXE9/DDaFt6Rr7iVZMldczhC0JNgTz+SHXT6CBHuX3e9SdB1Ua44oncaTWz7OBGLbCiK45wIDAQAB\n-----END PUBLIC KEY-----");

    private static readonly byte[] presetKey = "0CoJUm6Qyw8W8jud"u8.ToArray();
    private const string base62 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public override async Task<HttpRequestMessage> GenerateRequestMessageAsync(
        ApiHandlerOption option, CancellationToken cancellationToken = default)
    {
        return await GenerateRequestMessageAsync(ActualRequest!, option, cancellationToken).ConfigureAwait(false);
    }

    public override Task<HttpRequestMessage> GenerateRequestMessageAsync<TActualRequestModel>(
        TActualRequestModel actualRequest, ApiHandlerOption option, CancellationToken cancellationToken = default)
    {
        var url = Url;
        if (option.DegradeHttp)
            url = url.Replace("https://", "http://");
        url = Regex.Replace(url, @"\w*api", "weapi");
        var requestMessage = new HttpRequestMessage(Method, url);
        if (!string.IsNullOrWhiteSpace(option.XRealIP))
            requestMessage.Headers.Add("X-Real-IP", option.XRealIP);
        requestMessage.Headers.UserAgent.Clear();
        requestMessage.Headers.TryAddWithoutValidation("User-Agent",
                                                       UserAgentHelper.GetRandomUserAgent(
                                                           UserAgent ?? option.UserAgent));
        if (Url.Contains("music.163.com"))
            requestMessage.Headers.Referrer = new Uri("https://music.163.com");
        var cookies = option.Cookies.ToDictionary(t => t.Key, t => t.Value);
        foreach (var keyValuePair in Cookies)
        {
            cookies[keyValuePair.Key] = keyValuePair.Value;
        }

        if (cookies.Count > 0)
            requestMessage.Headers.Add("Cookie", string.Join("; ", cookies.Select(c => $"{c.Key}={c.Value}")));
        var req = actualRequest is not WeApiActualRequestBase wa ? new WeApiActualRequestBase() : wa;
        req.CsrfToken = cookies.GetValueOrDefault("__csrf", string.Empty);
        string json;
        if (req is TActualRequestModel ar)
            json = JsonSerializer.Serialize(ar, option.JsonSerializerOptions);
        else
            json = JsonSerializer.Serialize(req, option.JsonSerializerOptions);
        byte[] secretKey = new Random().RandomBytes(16);
        secretKey = secretKey.Select(n => (byte)base62[n % 62]).ToArray();
        var paramsData =
            AesEncrypt(
                AesEncrypt(json.ToByteArrayUtf8(), CipherMode.CBC, presetKey, iv).ToBase64String().ToByteArrayUtf8(),
                CipherMode.CBC, secretKey, iv).ToBase64String();
        var encSecKey = RsaEncrypt(secretKey.Reverse().ToArray(), publicKey).ToHexStringLower();
        var data = new Dictionary<string, string>()
                   {
                       { "params", paramsData },
                       { "encSecKey", encSecKey }
                   };

        requestMessage.Content = new FormUrlEncodedContent(data);
        return Task.FromResult(requestMessage);
    }

    public override async Task<Results<TResponse, ErrorResultBase>> ProcessResponseAsync(
        HttpResponseMessage response, ApiHandlerOption option,
        CancellationToken cancellationToken = default)
    {
        return await ProcessResponseAsync<TResponse>(response, option, cancellationToken).ConfigureAwait(false);
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
            return Results<TResponseModel, ErrorResultBase>
                   .CreateError(new ErrorResultBase(codedResponseBase.Code, "返回值不为 200")).WithValue(ret);
        return ret;
    }

    private static byte[] RsaEncrypt(byte[] buffer, RSAParameters key)
    {
        return GetByteArrayBigEndian(BigInteger.ModPow(GetBigIntegerBigEndian(buffer),
                                                       GetBigIntegerBigEndian(key.Exponent),
                                                       GetBigIntegerBigEndian(key.Modulus)));

        static byte[] GetByteArrayBigEndian(BigInteger value)
        {
            byte[] array = value.ToByteArray();
            if (array[array.Length - 1] == 0)
            {
                byte[] array2 = new byte[array.Length - 1];
                Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
                array = array2;
            }

            for (int i = 0; i < array.Length / 2; i++)
            {
                (array[i], array[array.Length - i - 1]) = (array[array.Length - i - 1], array[i]);
            }

            return array;
        }

        static BigInteger GetBigIntegerBigEndian(byte[] value)
        {
            byte[] value2 = new byte[value.Length + 1];
            for (int i = 0; i < value.Length; i++)
                value2[value2.Length - i - 2] = value[i];
            return new BigInteger(value2);
        }
    }

    private static byte[] AesEncrypt(byte[] buffer, CipherMode mode, byte[] key, byte[]? initVector = null)
    {
        using var aes = Aes.Create();
        aes.BlockSize = 128;
        aes.Key = key;
        if (initVector is not null)
            aes.IV = initVector;
        aes.Mode = mode;
        using var cryptoTransform = aes.CreateEncryptor();
        return cryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length);
    }


    private static RSAParameters ParsePublicKey(string publicKey)
    {
        publicKey = publicKey.Replace("\n", string.Empty);
        publicKey = publicKey.Substring(26, publicKey.Length - 50);
        using var stream = new MemoryStream(Convert.FromBase64String(publicKey));
        using var reader = new BinaryReader(stream);

        ushort i16 = reader.ReadUInt16();
        if (i16 == 0x8130)
            reader.ReadByte();
        else if (i16 == 0x8230)
            reader.ReadInt16();
        else
            throw new ArgumentException(nameof(publicKey));

        byte[] oid = reader.ReadBytes(15);
        if (!oid.SequenceEqual(new byte[]
                               {
                                   0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01,
                                   0x01, 0x05, 0x00
                               }))
            throw new ArgumentException(nameof(publicKey));

        i16 = reader.ReadUInt16();
        if (i16 == 0x8103)
            reader.ReadByte();
        else if (i16 == 0x8203)
            reader.ReadInt16();
        else
            throw new ArgumentException(nameof(publicKey));

        byte i8 = reader.ReadByte();
        if (i8 != 0x00)
            throw new ArgumentException(nameof(publicKey));
        i16 = reader.ReadUInt16();
        if (i16 == 0x8130)
            reader.ReadByte();
        else if (i16 == 0x8230)
            reader.ReadInt16();
        else
            throw new ArgumentException(nameof(publicKey));

        i16 = reader.ReadUInt16();
        byte high;
        byte low;
        if (i16 == 0x8102)
        {
            high = 0;
            low = reader.ReadByte();
        }
        else if (i16 == 0x8202)
        {
            high = reader.ReadByte();
            low = reader.ReadByte();
        }
        else
            throw new ArgumentException(nameof(publicKey));

        int modulusLength = BitConverter.ToInt32(new byte[] { low, high, 0x00, 0x00 }, 0);
        if (reader.PeekChar() == 0x00)
        {
            reader.ReadByte();
            modulusLength -= 1;
        }

        byte[] modulus = reader.ReadBytes(modulusLength);
        if (reader.ReadByte() != 0x02)
            throw new ArgumentException(nameof(publicKey));

        int exponentLength = reader.ReadByte();
        byte[] exponent = reader.ReadBytes(exponentLength);

        return new RSAParameters
               {
                   Modulus = modulus,
                   Exponent = exponent
               };
    }
}