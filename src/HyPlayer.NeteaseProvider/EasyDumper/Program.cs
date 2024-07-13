using System.Security.Cryptography;
using System.Text;
using HyPlayer.NeteaseApi.Extensions;
using Spectre.Console;

var currentMode = CurrentMode.EApiRequest;

AnsiConsole.Write(
    new FigletText("EasyDumper")
        .LeftJustified()
        .Color(Color.Green));

var rule = new Rule(currentMode.ToString());
AnsiConsole.Write(rule);

while (true)
{
    try
    {
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        if (currentMode is not CurrentMode.WeApiRequest)
            AnsiConsole.WriteLine("Please input encoded message, empty for enc.txt, 'switch' to switch mode");
        else
            AnsiConsole.MarkupLine("Please input [green]encSecKey[/], empty for enc.txt, 'switch' to switch mode");

        AnsiConsole.WriteLine();
        var encodedString = Console.ReadLine();
        byte[] encodedMessage;
        if (string.IsNullOrWhiteSpace(encodedString))
        {
            encodedMessage = File.ReadAllBytes("enc.txt");
        }
        else
        {
            encodedMessage = encodedString.ToByteArrayUtf8();
        }

        if (encodedString is null) continue;
        if (encodedString.Trim() == "switch")
        {
            currentMode = AnsiConsole.Prompt(new SelectionPrompt<CurrentMode>()
                                             .Title("Select your mode")
                                             .AddChoices(CurrentMode.EApiRequest, CurrentMode.EApiResponse,
                                                         CurrentMode.LinuxApiRequest, CurrentMode.LinuxApiResponse,
                                                         CurrentMode.WeApiRequest, CurrentMode.WeApiResponse));
            AnsiConsole.MarkupLine($"[green]Now switch to [orange1]{currentMode}[/] Mode[/]");
            AnsiConsole.Write(new Rule(currentMode.ToString()));
            continue;
        }
        
        switch (currentMode)
        {
            case CurrentMode.EApiRequest:
                encodedString = Encoding.UTF8.GetString(encodedMessage);
                encodedMessage = encodedString.Replace(
                                                  "\r", string.Empty)
                                              .Replace("\n", string.Empty)
                                              .Replace("params=", string.Empty)
                                              .ToLower().FromHex();
                Console.WriteLine(EApiDecode(encodedMessage));
                break;
            case CurrentMode.EApiResponse:
                Console.WriteLine(EApiDecode(encodedMessage));
                break;
            case CurrentMode.LinuxApiRequest:
                encodedString = Encoding.UTF8.GetString(encodedMessage);
                encodedMessage = encodedString.Replace(
                                                  "\r", string.Empty)
                                              .Replace("\n", string.Empty)
                                              .Replace("eparams=", string.Empty)
                                              .ToLower().FromHex();
                Console.WriteLine(LinuxApiDecode(encodedMessage));
                break;
            case CurrentMode.LinuxApiResponse:
                Console.WriteLine(LinuxApiDecode(encodedMessage));
                break;
            default:
                AnsiConsole.MarkupLine(
                    "[red] While it's using RsaEncrypt which requires private key to encrypt, cannot decrypt it");
                break;
        }
    }
    catch (Exception e)
    {
        AnsiConsole.WriteException(e);
    }
}


string LinuxApiDecode(byte[] encodedMessage)
{
    byte[] eapiKey = "rFgB&h#%2?^eDg:Q"u8.ToArray();
    using var aes = Aes.Create();
    aes.BlockSize = 128;
    aes.Key = eapiKey;
    aes.Mode = CipherMode.ECB;
    using var cryptoTransform = aes.CreateDecryptor();
    var resultBytes = cryptoTransform.TransformFinalBlock(encodedMessage, 0, encodedMessage.Length);
    return Encoding.UTF8.GetString(resultBytes);
}

string EApiDecode(byte[] encodedMessage)
{
    byte[] eapiKey = "e82ckenh8dichen8"u8.ToArray();
    using var aes = Aes.Create();
    aes.BlockSize = 128;
    aes.Key = eapiKey;
    aes.Mode = CipherMode.ECB;
    using var cryptoTransform = aes.CreateDecryptor();
    var resultBytes = cryptoTransform.TransformFinalBlock(encodedMessage, 0, encodedMessage.Length);
    return Encoding.UTF8.GetString(resultBytes);
}


public enum CurrentMode
{
    EApiRequest,
    EApiResponse,
    WeApiRequest,
    WeApiResponse,
    LinuxApiRequest,
    LinuxApiResponse
}

// Code from https://github.com/HMBSbige/CryptoBase/blob/master/src/CryptoBase/DataFormatExtensions/HexExtensions.cs
public static class HexExtensions
{
    /// <summary>
    /// Converts the specified string, which encodes binary data as hex characters, to an equivalent 8-bit unsigned integer array.
    /// </summary>
    public static byte[] FromHex(this string hex)
    {
        return hex.AsSpan().FromHex();
    }

    /// <summary>
    /// Converts the span, which encodes binary data as hex characters, to an equivalent 8-bit unsigned integer array.
    /// </summary>
    public static byte[] FromHex(this ReadOnlySpan<char> hex)
    {
        if ((hex.Length & 1) is not 0)
        {
            throw new ArgumentException($@"{nameof(hex)} length must be even");
        }

        int length = hex.Length >> 1;
        byte[] buffer = GC.AllocateUninitializedArray<byte>(length);

        for (int i = 0, j = 0; i < length; ++i, ++j)
        {
            // Convert first half of byte
            char c = hex[j];
            buffer[i] = (byte)((c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0')) << 4);

            // Convert second half of byte
            c = hex[++j];
            buffer[i] |= (byte)(c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0'));
        }

        return buffer;
    }
}