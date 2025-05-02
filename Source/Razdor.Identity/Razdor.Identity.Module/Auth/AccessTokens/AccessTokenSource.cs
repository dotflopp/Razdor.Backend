using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Razdor.Identity.Module.Auth.AccessTokens;

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
        var now = (ulong)(claims.CreationTime - _options.StartTime).TotalMilliseconds;

        var userIdBase64 = Base64Url.EncodeToString(
            Encoding.UTF8.GetBytes(claims.UserId.ToString())
        );
        var nowBase64 = Base64Url.EncodeToString(
            BitConverter.GetBytes(now)
        );

        var data = string.Join(".", userIdBase64, nowBase64);

        using var hasher = new HMACSHA256(_options.Key);
        var signature = hasher.ComputeHash(Encoding.UTF8.GetBytes(data));

        var signatureBase64 = Base64Url.EncodeToString(signature);
        return string.Join(".", data, signatureBase64);
    }

    public bool Check(string token)
    {
        var match = _dataAndSignatureExtractorRegex.Match(token);
        if (!match.Success)
            return false;

        var data = match.Groups[1].Value;
        var originalSignature = match.Groups[2].Value;

        using var hasher = new HMACSHA256(_options.Key);
        var signature = hasher.ComputeHash(Encoding.UTF8.GetBytes(data));
        var signatureBase64 = Base64Url.EncodeToString(signature);

        return signatureBase64 == originalSignature;
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
        var tokenData = token.Split(".");
        if (tokenData.Length != 3)
            throw new ArgumentException("Invalid token format");

        var userIdBase64 = tokenData[0];
        var nowBase64 = tokenData[1];

        var userId = Encoding.UTF8.GetString(
            Base64Url.DecodeFromChars(userIdBase64)
        );
        var realativeCreationTime = BitConverter.ToUInt64(
            Base64Url.DecodeFromChars(nowBase64)
        );

        DateTimeOffset absoluteCreationTime = _options.StartTime.AddMilliseconds(realativeCreationTime);

        return new TokenClaims(
            Convert.ToUInt64(userId),
            absoluteCreationTime
        );
    }
}