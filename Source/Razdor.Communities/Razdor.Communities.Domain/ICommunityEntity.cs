﻿using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain;

public interface ICommunityEntity<out T> : IEntity<T>
{
    ulong CommunityId { get; }
}