namespace Razdor.Communities.Domain;

public interface INamed
{
    string Name { get; }
    void Rename(string newName);
}