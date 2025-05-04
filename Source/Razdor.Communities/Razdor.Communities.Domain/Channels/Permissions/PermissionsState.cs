namespace Razdor.Communities.Domain.Channels;

public enum PermissionsState : byte
{
    /// <summary>
    /// Право определяется ролью пользователя
    /// </summary>
    Default = 0,
    /// <summary>
    /// Даже если у пользователя было разрешение оно будет отозвано
    /// </summary>
    Rejected = 0b01,
    /// <summary>
    /// Если у пользователя нет разрешение, оно ему будет предоставлено
    /// </summary>
    Granted = 0b10
}