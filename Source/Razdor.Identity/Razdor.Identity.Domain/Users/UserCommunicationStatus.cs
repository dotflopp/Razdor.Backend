namespace Razdor.Identity.Domain.Users;

[Serializable]
public enum UserCommunicationStatus
{
    Online = 0x1,
    Offline = 0x2,
    DoNotDisturb = 0x4,
    Invisible = 0x8,
}