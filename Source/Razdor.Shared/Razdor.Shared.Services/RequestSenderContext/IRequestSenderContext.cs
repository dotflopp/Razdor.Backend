using System.Diagnostics.CodeAnalysis;

namespace Razdor.Shared.Module.RequestSenderContext;

public interface IRequestSenderContext
{
    UserClaims User { get; }

    bool IsAuthenticated { get; }
}