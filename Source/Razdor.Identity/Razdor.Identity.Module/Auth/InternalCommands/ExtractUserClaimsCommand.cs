using Razdor.Identity.Module.Contracts;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Identity.Module.Auth.InternalCommands;

/// <summary>
///     Из Токена доступа вернутся утверждения о пользователе, если токен испорчен вернется исключение
/// </summary>
public record ExtractUserClaimsCommand(
    string AccessToken
) : IIdentityCommand<UserClaims>;