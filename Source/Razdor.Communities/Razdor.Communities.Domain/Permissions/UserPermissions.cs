using System.Text.Json.Serialization;

namespace Razdor.Communities.Domain.Permissions;

[Flags]
public enum UserPermissions : ulong
{
    None = 0,
    /// <summary>
    ///     Дает карт-бланш на свинство
    /// </summary>
    Administrator = 0x1,
    /// <summary>
    ///     Позволяет изменять параметры сообщества
    /// </summary>
    ManageCommunity = 0x2,
    /// <summary>
    ///     Позволяет изменять параметры каналов
    /// </summary>
    ManageChannel = 0x4,
    /// <summary>
    ///     Позволяет управлять ролями, добавлять и удалять роли у пользователя
    /// </summary>
    ManageRole = 0x8,
    /// <summary>
    ///     Позволяет просматривать журнал событий
    /// </summary>
    ViewAuditLogs = 0x10,
    /// <summary>
    ///     Позволяет участнику просматривать каналы сообщества
    /// </summary>
    ViewChannel = 0x20,
    /// <summary>
    ///     Позволяет отправлять сообщения в текстовый канал сообщества
    /// </summary>
    SendMessage = 0x80,
    /// <summary>
    ///     Позволяет закреплять и удалять чужие сообщения в текстовом канале сообщества
    /// </summary>
    ManageMessages = 0x100,
    /// <summary>
    ///     Позволяет прикреплять файлы к сообщению
    /// </summary>
    AttachFiles = 0x200,
    /// <summary>
    ///     Позволяет прикреплять Embed к сообщению
    /// </summary>
    AttachEmbed = 0x400,
    /// <summary>
    ///     Позволяет использовать эмомдзи в чате
    /// </summary>
    UseEmoji = 0x800,
    /// <summary>
    ///     Использовать упоминания всех пользователей
    /// </summary>
    MentionEveryone = 0x2000,
    /// <summary>
    ///     Позволяет подключаться к голосовому каналу
    /// </summary>
    Connect = 0x4000,
    /// <summary>
    ///     Позволяет говорить в голосовом канале
    /// </summary>
    Speak = 0x8000,
    /// <summary>
    ///     Позволяет глушить пользователей в голосовом канале
    /// </summary>
    MuteMembers = 0x10000,
    /// <summary>
    ///     Позволяет отключать пользователям звук в голосовом канале
    /// </summary>
    DeafenMembers = 0x20000,
    /// <summary>
    ///     Позволяет перемещать пользователей в голосовых каналах
    /// </summary>
    MoveMembers = 0x40000,
    /// <summary>
    ///     Позволяет исключать участников
    /// </summary>
    KickMembers = 0x80000,
    /// <summary>
    ///     Позволяет банить участников
    /// </summary>
    BanMembers = 0x100000,
    /// <summary>
    ///     Позволяет создавать приглашения для новых участников
    /// </summary>
    InviteMembers = 0x200000,
    /// <summary>
    ///     Позволяет пользователю менять свой псевдоним в сообществе
    /// </summary>
    ChangeNickname = 0x400000,
    /// <summary>
    ///     Позволяет менять чужие псевдонимы в сообществе
    /// </summary>
    ManageNicknames = 0x800000,

    /// <summary>
    ///     Позволяет архивировать, удалять ответвления канала
    /// </summary>
    ManageFork = 0x1000000,
    /// <summary>
    ///     Позволяет создавать ответвления от основного диалога
    /// </summary>
    CreateFork = 0x8000,
    /// <summary>
    ///     Позволяет отправлять сообщения в ответвление
    /// </summary>
    SendMessageInFork = 0x10000,
    /// <summary>
    ///     Для ленивого вычисления прав создателя сообщества
    /// </summary>
    All = ulong.MaxValue
}