using System.Security.Cryptography;
using System.Text;

namespace Razdor.Identity.Domain;

public record AuthenticationTokenFactoryOptions(
    DateTime StartTime,
    byte[] Key
);

public class AuthenticationTokenFactory(
    AuthenticationTokenFactoryOptions options
){
    public string CreateNew(UserAccount user)
    {
        if (user.IsTransient)
            throw new ArgumentException($"Cannot create token for transient user {user.Id}");
        
        ulong now = (ulong)(DateTime.UtcNow - options.StartTime).TotalMilliseconds;
        
        string userIdBase64 = Convert.ToBase64String(
            BitConverter.GetBytes(user.Id)
        );
        string nowBase64 = Convert.ToBase64String(
            BitConverter.GetBytes(now)
        );
        
        string data = string.Join(".", userIdBase64, nowBase64);
        
        using HMACSHA256 hasher = new HMACSHA256(options.Key);
        byte[] signature = hasher.ComputeHash(Encoding.UTF8.GetBytes(data));
        
        string signatureBase64 = Convert.ToBase64String(signature);
        
        return string.Join(data, signatureBase64); 
    }
}