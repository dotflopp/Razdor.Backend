namespace Razdor.Shared.Module.Identities;

public interface IServiceIdentity
{
    bool IsAuthenticated { get; }
    UserIdentity? User { get; }
}