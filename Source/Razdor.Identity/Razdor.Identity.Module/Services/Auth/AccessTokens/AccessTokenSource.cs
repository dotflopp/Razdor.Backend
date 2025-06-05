using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Razdor.Identity.Module.Services.Auth.AccessTokens;

public record AccessTokenSourceOptions(
    DateTime StartTime,
    byte[] Key
);

public partial class AccessTokenSource
{
    private readonly Regex _dataAndSignatureExtractorRegex;
    private readonly AccessTokenSourceOptions _options;

    public AccessTokenSource(AccessTokenSourceOptions options)
    {
        _options = options;
        _dataAndSignatureExtractorRegex = CreateDataAndSignatureExtractRegex();
    }

    public string CreateNew(TokenClaims claims)
    {
        ulong now = (ulong)(claims.CreationTime - _options.StartTime).TotalMilliseconds;

            string userIdBase64 = Base64Url.EncodeToString(
            Encoding.UTF8.GetBytes(claims.UserId.ToString())
        );
        string nowBase64 = Base64Url.EncodeToString(
            BitConverter.GetBytes(now)
        );

        string data = string.Join(".", userIdBase64, nowBase64);

        using var hasher = new HMACSHA256(_options.Key);
        byte[] signature = hasher.ComputeHash(Encoding.UTF8.GetBytes(data));

        string signatureBase64 = Base64Url.EncodeToString(signature);
        return string.Join(".", data, signatureBase64);
    }

    public bool Check(string token)
    {
        Match match = _dataAndSignatureExtractorRegex.Match(token);
        if (!match.Success)
            return false;

        string data = match.Groups[1].Value;
        string originalSignature = match.Groups[2].Value;

        using var hasher = new HMACSHA256(_options.Key);
        byte[] signature = hasher.ComputeHash(Encoding.UTF8.GetBytes(data));
        string signatureBase64 = Base64Url.EncodeToString(signature);

        return signatureBase64.Equals(originalSignature);
    }

    [GeneratedRegex(
        @"^(.*\..*)\.(.*)$",
        RegexOptions.Compiled
        | RegexOptions.IgnorePatternWhitespace
        | RegexOptions.Singleline
    )]
    private partial Regex CreateDataAndSignatureExtractRegex();

    public TokenClaims Read(string token)
    {
        string[] tokenData = token.Split(".");
        if (tokenData.Length != 3)
            throw new ArgumentException("Invalid token format");

        string userIdBase64 = tokenData[0];
        string nowBase64 = tokenData[1];

        string userId = Encoding.UTF8.GetString(
            Base64Url.DecodeFromChars(userIdBase64)
        );
        ulong realativeCreationTime = BitConverter.ToUInt64(
            Base64Url.DecodeFromChars(nowBase64)
        );

        DateTimeOffset absoluteCreationTime = _options.StartTime.AddMilliseconds(realativeCreationTime);

        return new TokenClaims(
            Convert.ToUInt64(userId),
            absoluteCreationTime
        );
    }
}