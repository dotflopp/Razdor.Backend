using System.Diagnostics.CodeAnalysis;
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

namespace Razdor.RestApi.AuthenticationScheme;

public partial class AccessTokenAuthenticationHandler : AuthenticationHandler<AccessTokenAuthenticationOptions>
{
    private readonly Regex _bearerTokenRegex;

    public AccessTokenAuthenticationHandler(
        IOptionsMonitor<AccessTokenAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder
    ) : base(options, logger, encoder)
    {
        _bearerTokenRegex = CreateBearerTokenRegex();
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string? accessToken = ExtractTokenFromHeader();

        accessToken ??= ExtractTokenFromRoute();
        
        if (accessToken == null)
            return AuthenticateResult.NoResult();
        
        return await ValidateTokenAsync(accessToken);
    }
    
    private string? ExtractTokenFromRoute()
    {
        if (Context.Request.Query.TryGetValue("access-token", out StringValues token))
            return token.ToString();
        
        return null;
    }

    private string? ExtractTokenFromHeader()
    {
        if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out StringValues authorization))
            return null;
        
        Match headerMatch = _bearerTokenRegex.Match(authorization.ToString());
        return headerMatch.Success
            ? headerMatch.Groups[2].Value
            : null;
    }

    private async Task<AuthenticateResult> ValidateTokenAsync(string accessToken)
    {
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
    private partial Regex CreateBearerTokenRegex();
}