using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Services.Auth.InternalCommands;
using Razdor.Identity.Module.Services.Auth.InternalCommands.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Api.AuthenticationScheme;

public partial class AccessTokenAuthenticationHandler : AuthenticationHandler<AccessTokenAuthenticationOptions>
{
    private readonly Regex _authenticationHeaderRegex;

    public AccessTokenAuthenticationHandler(
        IOptionsMonitor<AccessTokenAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder
    ) : base(options, logger, encoder)
    {
        _authenticationHeaderRegex = CreateAuthenticationHeaderRegex();
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out StringValues authorization))
            return AuthenticateResult.NoResult();

        Match headerMatch = _authenticationHeaderRegex.Match(authorization.ToString());

        if (!headerMatch.Success)
            return AuthenticateResult.Fail("Invalid authorization header");

        string accessToken = headerMatch.Groups[2].Value;
        IIdentityModule identity = Context.RequestServices.GetRequiredService<IIdentityModule>();

        UserClaims userClaims;
        try
        {
            userClaims = await identity.ExecuteCommandAsync(new ExtractUserClaimsCommand(
                accessToken
            ));
        }
        catch (InvalidAccessTokenException exception)
        {
            return AuthenticateResult.Fail(exception);
        }

        var claim = new Claim(ClaimTypes.NameIdentifier, userClaims.Id.ToString());
        var claimsIdentity = new ClaimsIdentity([claim], nameof(AccessTokenAuthenticationHandler));
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

    [GeneratedRegex(
        @"(Bearer|Bot)\s+([^.]*\.[^.]*\.[^.]*)",
        RegexOptions.Compiled
    )]
    private partial Regex CreateAuthenticationHeaderRegex();
}