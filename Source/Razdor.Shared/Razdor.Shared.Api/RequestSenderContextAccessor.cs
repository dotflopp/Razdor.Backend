using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Razdor.Shared.Module.RequestSenderContext;

namespace ClassLibrary1;

public class RequestSenderContextAccessor(IHttpContextAccessor httpContextAccessor) : IRequestSenderContext
{
    private UserClaims? _user;

    public UserClaims? User
    {
        get
        {
            if (_user != null)
                return _user;

            _user = ExtractUserClaims();
            return _user;
        }
    }

    public bool IsAuthenticated
    {
        get
        {
            if (_user != null)
                return true;

            _user = ExtractUserClaims();
            return _user != null;
        }
    }

    private UserClaims? ExtractUserClaims()
    {
        var claims = httpContextAccessor.HttpContext?.User?.Claims ?? [];

        var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return null;

        return new UserClaims(Convert.ToUInt64(userId));
    }
}