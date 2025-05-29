using System.Security.Claims;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Shared.Api;

public class RequestSenderContextAccessor(IHttpContextAccessor httpContextAccessor) : IRequestSenderContextAccessor
{
    private UserClaims? _user;

    public UserClaims User
    {
        get
        {
            if (_user != null)
                return _user;

            _user = ExtractUserClaims();

            if (_user == null)
                throw new InvalidOperationException("There may be no authorization check, or an middleware that adds claims is not registered.");

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
        IEnumerable<Claim> claims = httpContextAccessor.HttpContext?.User?.Claims ?? [];

        string? userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return null;

        return new UserClaims(Convert.ToUInt64(userId));
    }
}