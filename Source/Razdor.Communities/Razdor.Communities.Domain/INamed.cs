namespace Razdor.Communities.Domain.Permissions;

public interface INamed
{
    string Name { get; }
    void Rename(string newName);
}