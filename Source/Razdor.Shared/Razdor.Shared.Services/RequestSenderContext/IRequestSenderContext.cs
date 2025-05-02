using System.Diagnostics.CodeAnalysis;

namespace Razdor.Shared.Module.RequestSenderContext;

public interface IRequestSenderContext
{
    UserClaims? User { get; }

    [MemberNotNullWhen(true, nameof(User))]
    bool IsAuthenticated { get; }
}

