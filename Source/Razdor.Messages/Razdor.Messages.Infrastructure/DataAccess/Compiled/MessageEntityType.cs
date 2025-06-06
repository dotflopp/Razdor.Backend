﻿// <auto-generated />
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using MongoDB.EntityFrameworkCore.Storage;
using Razdor.Messages.Domain;
using Razdor.Shared.Domain;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Razdor.Messages.Infrastructure.DataAccess
{
    [EntityFrameworkInternal]
    public partial class MessageEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Razdor.Messages.Domain.Message",
                typeof(Message),
                baseEntityType,
                propertyCount: 7,
                navigationCount: 4,
                keyCount: 1);

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(ulong),
                propertyInfo: typeof(BaseSnowflakeEntity).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(BaseSnowflakeEntity).GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                afterSaveBehavior: PropertySaveBehavior.Throw,
                sentinel: 0ul);
            id.SetGetter(
                ulong (Message entity) => BaseSnowflakeEntityUnsafeAccessors.Id(entity),
                bool (Message entity) => BaseSnowflakeEntityUnsafeAccessors.Id(entity) == 0UL,
                ulong (Message instance) => BaseSnowflakeEntityUnsafeAccessors.Id(instance),
                bool (Message instance) => BaseSnowflakeEntityUnsafeAccessors.Id(instance) == 0UL);
            id.SetSetter(
                (Message entity, ulong value) => BaseSnowflakeEntityUnsafeAccessors.Id(entity) = value);
            id.SetMaterializationSetter(
                (Message entity, ulong value) => BaseSnowflakeEntityUnsafeAccessors.Id(entity) = value);
            id.SetAccessors(
                ulong (InternalEntityEntry entry) => BaseSnowflakeEntityUnsafeAccessors.Id(((Message)(entry.Entity))),
                ulong (InternalEntityEntry entry) => BaseSnowflakeEntityUnsafeAccessors.Id(((Message)(entry.Entity))),
                ulong (InternalEntityEntry entry) => entry.ReadOriginalValue<ulong>(id, 0),
                ulong (InternalEntityEntry entry) => entry.ReadRelationshipSnapshotValue<ulong>(id, 0),
                object (ValueBuffer valueBuffer) => valueBuffer[0]);
            id.SetPropertyIndexes(
                index: 0,
                originalValueIndex: 0,
                shadowIndex: -1,
                relationshipIndex: 0,
                storeGenerationIndex: -1);
            id.TypeMapping = MongoTypeMapping.Default.Clone(
                comparer: new ValueComparer<ulong>(
                    bool (ulong v1, ulong v2) => v1 == v2,
                    int (ulong v) => ((object)v).GetHashCode(),
                    ulong (ulong v) => v),
                keyComparer: new ValueComparer<ulong>(
                    bool (ulong v1, ulong v2) => v1 == v2,
                    int (ulong v) => ((object)v).GetHashCode(),
                    ulong (ulong v) => v),
                providerValueComparer: new ValueComparer<ulong>(
                    bool (ulong v1, ulong v2) => v1 == v2,
                    int (ulong v) => ((object)v).GetHashCode(),
                    ulong (ulong v) => v),
                clrType: typeof(ulong));
            id.SetCurrentValueComparer(new EntryCurrentValueComparer<ulong>(id));
            id.AddAnnotation("Mongo:ElementName", "_id");

            var channelId = runtimeEntityType.AddProperty(
                "ChannelId",
                typeof(ulong),
                propertyInfo: typeof(Message).GetProperty("ChannelId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Message).GetField("<ChannelId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: 0ul);
            channelId.SetGetter(
                ulong (Message entity) => MessageUnsafeAccessors.ChannelId(entity),
                bool (Message entity) => MessageUnsafeAccessors.ChannelId(entity) == 0UL,
                ulong (Message instance) => MessageUnsafeAccessors.ChannelId(instance),
                bool (Message instance) => MessageUnsafeAccessors.ChannelId(instance) == 0UL);
            channelId.SetSetter(
                (Message entity, ulong value) => MessageUnsafeAccessors.ChannelId(entity) = value);
            channelId.SetMaterializationSetter(
                (Message entity, ulong value) => MessageUnsafeAccessors.ChannelId(entity) = value);
            channelId.SetAccessors(
                ulong (InternalEntityEntry entry) => MessageUnsafeAccessors.ChannelId(((Message)(entry.Entity))),
                ulong (InternalEntityEntry entry) => MessageUnsafeAccessors.ChannelId(((Message)(entry.Entity))),
                ulong (InternalEntityEntry entry) => entry.ReadOriginalValue<ulong>(channelId, 1),
                ulong (InternalEntityEntry entry) => entry.GetCurrentValue<ulong>(channelId),
                object (ValueBuffer valueBuffer) => valueBuffer[1]);
            channelId.SetPropertyIndexes(
                index: 1,
                originalValueIndex: 1,
                shadowIndex: -1,
                relationshipIndex: -1,
                storeGenerationIndex: -1);
            channelId.TypeMapping = MongoTypeMapping.Default.Clone(
                comparer: new ValueComparer<ulong>(
                    bool (ulong v1, ulong v2) => v1 == v2,
                    int (ulong v) => ((object)v).GetHashCode(),
                    ulong (ulong v) => v),
                keyComparer: new ValueComparer<ulong>(
                    bool (ulong v1, ulong v2) => v1 == v2,
                    int (ulong v) => ((object)v).GetHashCode(),
                    ulong (ulong v) => v),
                providerValueComparer: new ValueComparer<ulong>(
                    bool (ulong v1, ulong v2) => v1 == v2,
                    int (ulong v) => ((object)v).GetHashCode(),
                    ulong (ulong v) => v),
                clrType: typeof(ulong));

            var createdAt = runtimeEntityType.AddProperty(
                "CreatedAt",
                typeof(DateTimeOffset),
                propertyInfo: typeof(Message).GetProperty("CreatedAt", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Message).GetField("<CreatedAt>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
            createdAt.SetGetter(
                DateTimeOffset (Message entity) => MessageUnsafeAccessors.CreatedAt(entity),
                bool (Message entity) => MessageUnsafeAccessors.CreatedAt(entity).EqualsExact(default(DateTimeOffset)),
                DateTimeOffset (Message instance) => MessageUnsafeAccessors.CreatedAt(instance),
                bool (Message instance) => MessageUnsafeAccessors.CreatedAt(instance).EqualsExact(default(DateTimeOffset)));
            createdAt.SetSetter(
                (Message entity, DateTimeOffset value) => MessageUnsafeAccessors.CreatedAt(entity) = value);
            createdAt.SetMaterializationSetter(
                (Message entity, DateTimeOffset value) => MessageUnsafeAccessors.CreatedAt(entity) = value);
            createdAt.SetAccessors(
                DateTimeOffset (InternalEntityEntry entry) => MessageUnsafeAccessors.CreatedAt(((Message)(entry.Entity))),
                DateTimeOffset (InternalEntityEntry entry) => MessageUnsafeAccessors.CreatedAt(((Message)(entry.Entity))),
                DateTimeOffset (InternalEntityEntry entry) => entry.ReadOriginalValue<DateTimeOffset>(createdAt, 2),
                DateTimeOffset (InternalEntityEntry entry) => entry.GetCurrentValue<DateTimeOffset>(createdAt),
                object (ValueBuffer valueBuffer) => valueBuffer[2]);
            createdAt.SetPropertyIndexes(
                index: 2,
                originalValueIndex: 2,
                shadowIndex: -1,
                relationshipIndex: -1,
                storeGenerationIndex: -1);
            createdAt.TypeMapping = MongoTypeMapping.Default.Clone(
                comparer: new ValueComparer<DateTimeOffset>(
                    bool (DateTimeOffset v1, DateTimeOffset v2) => v1.EqualsExact(v2),
                    int (DateTimeOffset v) => ((object)v).GetHashCode(),
                    DateTimeOffset (DateTimeOffset v) => v),
                keyComparer: new ValueComparer<DateTimeOffset>(
                    bool (DateTimeOffset v1, DateTimeOffset v2) => v1.EqualsExact(v2),
                    int (DateTimeOffset v) => ((object)v).GetHashCode(),
                    DateTimeOffset (DateTimeOffset v) => v),
                providerValueComparer: new ValueComparer<DateTimeOffset>(
                    bool (DateTimeOffset v1, DateTimeOffset v2) => v1.EqualsExact(v2),
                    int (DateTimeOffset v) => ((object)v).GetHashCode(),
                    DateTimeOffset (DateTimeOffset v) => v),
                clrType: typeof(DateTimeOffset));

            var editedAt = runtimeEntityType.AddProperty(
                "EditedAt",
                typeof(DateTimeOffset?),
                propertyInfo: typeof(Message).GetProperty("EditedAt", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Message).GetField("<EditedAt>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            editedAt.SetGetter(
                DateTimeOffset? (Message entity) => MessageUnsafeAccessors.EditedAt(entity),
                bool (Message entity) => !(MessageUnsafeAccessors.EditedAt(entity).HasValue),
                DateTimeOffset? (Message instance) => MessageUnsafeAccessors.EditedAt(instance),
                bool (Message instance) => !(MessageUnsafeAccessors.EditedAt(instance).HasValue));
            editedAt.SetSetter(
                (Message entity, DateTimeOffset? value) => MessageUnsafeAccessors.EditedAt(entity) = value);
            editedAt.SetMaterializationSetter(
                (Message entity, DateTimeOffset? value) => MessageUnsafeAccessors.EditedAt(entity) = value);
            editedAt.SetAccessors(
                DateTimeOffset? (InternalEntityEntry entry) => MessageUnsafeAccessors.EditedAt(((Message)(entry.Entity))),
                DateTimeOffset? (InternalEntityEntry entry) => MessageUnsafeAccessors.EditedAt(((Message)(entry.Entity))),
                DateTimeOffset? (InternalEntityEntry entry) => entry.ReadOriginalValue<DateTimeOffset?>(editedAt, 3),
                DateTimeOffset? (InternalEntityEntry entry) => entry.GetCurrentValue<DateTimeOffset?>(editedAt),
                object (ValueBuffer valueBuffer) => valueBuffer[3]);
            editedAt.SetPropertyIndexes(
                index: 3,
                originalValueIndex: 3,
                shadowIndex: -1,
                relationshipIndex: -1,
                storeGenerationIndex: -1);
            editedAt.TypeMapping = MongoTypeMapping.Default.Clone(
                comparer: new ValueComparer<DateTimeOffset>(
                    bool (DateTimeOffset v1, DateTimeOffset v2) => v1.EqualsExact(v2),
                    int (DateTimeOffset v) => ((object)v).GetHashCode(),
                    DateTimeOffset (DateTimeOffset v) => v),
                keyComparer: new ValueComparer<DateTimeOffset>(
                    bool (DateTimeOffset v1, DateTimeOffset v2) => v1.EqualsExact(v2),
                    int (DateTimeOffset v) => ((object)v).GetHashCode(),
                    DateTimeOffset (DateTimeOffset v) => v),
                providerValueComparer: new ValueComparer<DateTimeOffset>(
                    bool (DateTimeOffset v1, DateTimeOffset v2) => v1.EqualsExact(v2),
                    int (DateTimeOffset v) => ((object)v).GetHashCode(),
                    DateTimeOffset (DateTimeOffset v) => v),
                clrType: typeof(DateTimeOffset));
            editedAt.SetComparer(new NullableValueComparer<DateTimeOffset>(editedAt.TypeMapping.Comparer));
            editedAt.SetKeyComparer(new NullableValueComparer<DateTimeOffset>(editedAt.TypeMapping.KeyComparer));

            var isPinned = runtimeEntityType.AddProperty(
                "IsPinned",
                typeof(bool),
                propertyInfo: typeof(Message).GetProperty("IsPinned", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Message).GetField("<IsPinned>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: false);
            isPinned.SetGetter(
                bool (Message entity) => MessageUnsafeAccessors.IsPinned(entity),
                bool (Message entity) => MessageUnsafeAccessors.IsPinned(entity) == false,
                bool (Message instance) => MessageUnsafeAccessors.IsPinned(instance),
                bool (Message instance) => MessageUnsafeAccessors.IsPinned(instance) == false);
            isPinned.SetSetter(
                (Message entity, bool value) => MessageUnsafeAccessors.IsPinned(entity) = value);
            isPinned.SetMaterializationSetter(
                (Message entity, bool value) => MessageUnsafeAccessors.IsPinned(entity) = value);
            isPinned.SetAccessors(
                bool (InternalEntityEntry entry) => MessageUnsafeAccessors.IsPinned(((Message)(entry.Entity))),
                bool (InternalEntityEntry entry) => MessageUnsafeAccessors.IsPinned(((Message)(entry.Entity))),
                bool (InternalEntityEntry entry) => entry.ReadOriginalValue<bool>(isPinned, 4),
                bool (InternalEntityEntry entry) => entry.GetCurrentValue<bool>(isPinned),
                object (ValueBuffer valueBuffer) => valueBuffer[4]);
            isPinned.SetPropertyIndexes(
                index: 4,
                originalValueIndex: 4,
                shadowIndex: -1,
                relationshipIndex: -1,
                storeGenerationIndex: -1);
            isPinned.TypeMapping = MongoTypeMapping.Default.Clone(
                comparer: new ValueComparer<bool>(
                    bool (bool v1, bool v2) => v1 == v2,
                    int (bool v) => ((object)v).GetHashCode(),
                    bool (bool v) => v),
                keyComparer: new ValueComparer<bool>(
                    bool (bool v1, bool v2) => v1 == v2,
                    int (bool v) => ((object)v).GetHashCode(),
                    bool (bool v) => v),
                providerValueComparer: new ValueComparer<bool>(
                    bool (bool v1, bool v2) => v1 == v2,
                    int (bool v) => ((object)v).GetHashCode(),
                    bool (bool v) => v),
                clrType: typeof(bool));

            var text = runtimeEntityType.AddProperty(
                "Text",
                typeof(string),
                propertyInfo: typeof(Message).GetProperty("Text", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Message).GetField("<Text>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            text.SetGetter(
                string (Message entity) => MessageUnsafeAccessors.Text(entity),
                bool (Message entity) => MessageUnsafeAccessors.Text(entity) == null,
                string (Message instance) => MessageUnsafeAccessors.Text(instance),
                bool (Message instance) => MessageUnsafeAccessors.Text(instance) == null);
            text.SetSetter(
                (Message entity, string value) => MessageUnsafeAccessors.Text(entity) = value);
            text.SetMaterializationSetter(
                (Message entity, string value) => MessageUnsafeAccessors.Text(entity) = value);
            text.SetAccessors(
                string (InternalEntityEntry entry) => MessageUnsafeAccessors.Text(((Message)(entry.Entity))),
                string (InternalEntityEntry entry) => MessageUnsafeAccessors.Text(((Message)(entry.Entity))),
                string (InternalEntityEntry entry) => entry.ReadOriginalValue<string>(text, 5),
                string (InternalEntityEntry entry) => entry.GetCurrentValue<string>(text),
                object (ValueBuffer valueBuffer) => valueBuffer[5]);
            text.SetPropertyIndexes(
                index: 5,
                originalValueIndex: 5,
                shadowIndex: -1,
                relationshipIndex: -1,
                storeGenerationIndex: -1);
            text.TypeMapping = MongoTypeMapping.Default.Clone(
                comparer: new ValueComparer<string>(
                    bool (string v1, string v2) => v1 == v2,
                    int (string v) => ((object)v).GetHashCode(),
                    string (string v) => v),
                keyComparer: new ValueComparer<string>(
                    bool (string v1, string v2) => v1 == v2,
                    int (string v) => ((object)v).GetHashCode(),
                    string (string v) => v),
                providerValueComparer: new ValueComparer<string>(
                    bool (string v1, string v2) => v1 == v2,
                    int (string v) => ((object)v).GetHashCode(),
                    string (string v) => v),
                clrType: typeof(string));

            var userId = runtimeEntityType.AddProperty(
                "UserId",
                typeof(ulong),
                propertyInfo: typeof(Message).GetProperty("UserId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Message).GetField("<UserId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: 0ul);
            userId.SetGetter(
                ulong (Message entity) => MessageUnsafeAccessors.UserId(entity),
                bool (Message entity) => MessageUnsafeAccessors.UserId(entity) == 0UL,
                ulong (Message instance) => MessageUnsafeAccessors.UserId(instance),
                bool (Message instance) => MessageUnsafeAccessors.UserId(instance) == 0UL);
            userId.SetSetter(
                (Message entity, ulong value) => MessageUnsafeAccessors.UserId(entity) = value);
            userId.SetMaterializationSetter(
                (Message entity, ulong value) => MessageUnsafeAccessors.UserId(entity) = value);
            userId.SetAccessors(
                ulong (InternalEntityEntry entry) => MessageUnsafeAccessors.UserId(((Message)(entry.Entity))),
                ulong (InternalEntityEntry entry) => MessageUnsafeAccessors.UserId(((Message)(entry.Entity))),
                ulong (InternalEntityEntry entry) => entry.ReadOriginalValue<ulong>(userId, 6),
                ulong (InternalEntityEntry entry) => entry.GetCurrentValue<ulong>(userId),
                object (ValueBuffer valueBuffer) => valueBuffer[6]);
            userId.SetPropertyIndexes(
                index: 6,
                originalValueIndex: 6,
                shadowIndex: -1,
                relationshipIndex: -1,
                storeGenerationIndex: -1);
            userId.TypeMapping = MongoTypeMapping.Default.Clone(
                comparer: new ValueComparer<ulong>(
                    bool (ulong v1, ulong v2) => v1 == v2,
                    int (ulong v) => ((object)v).GetHashCode(),
                    ulong (ulong v) => v),
                keyComparer: new ValueComparer<ulong>(
                    bool (ulong v1, ulong v2) => v1 == v2,
                    int (ulong v) => ((object)v).GetHashCode(),
                    ulong (ulong v) => v),
                providerValueComparer: new ValueComparer<ulong>(
                    bool (ulong v1, ulong v2) => v1 == v2,
                    int (ulong v) => ((object)v).GetHashCode(),
                    ulong (ulong v) => v),
                clrType: typeof(ulong));

            var key = runtimeEntityType.AddKey(
                new[] { id });
            runtimeEntityType.SetPrimaryKey(key);

            return runtimeEntityType;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            var id = runtimeEntityType.FindProperty("Id");
            var channelId = runtimeEntityType.FindProperty("ChannelId");
            var createdAt = runtimeEntityType.FindProperty("CreatedAt");
            var editedAt = runtimeEntityType.FindProperty("EditedAt");
            var isPinned = runtimeEntityType.FindProperty("IsPinned");
            var text = runtimeEntityType.FindProperty("Text");
            var userId = runtimeEntityType.FindProperty("UserId");
            var key = runtimeEntityType.FindKey(new[] { id });
            key.SetPrincipalKeyValueFactory(KeyValueFactoryFactory.CreateSimpleNonNullableFactory<ulong>(key));
            key.SetIdentityMapFactory(IdentityMapFactoryFactory.CreateFactory<ulong>(key));
            var embed = runtimeEntityType.FindNavigation("Embed");
            var mentions = runtimeEntityType.FindNavigation("Mentions");
            var reference = runtimeEntityType.FindNavigation("Reference");
            var _attachments = runtimeEntityType.FindNavigation("_attachments");
            runtimeEntityType.SetOriginalValuesFactory(
                ISnapshot (InternalEntityEntry source) =>
                {
                    var entity = ((Message)(source.Entity));
                    return ((ISnapshot)(new Snapshot<ulong, ulong, DateTimeOffset, DateTimeOffset?, bool, string, ulong>(((ValueComparer<ulong>)(((IProperty)id).GetValueComparer())).Snapshot(source.GetCurrentValue<ulong>(id)), ((ValueComparer<ulong>)(((IProperty)channelId).GetValueComparer())).Snapshot(source.GetCurrentValue<ulong>(channelId)), ((ValueComparer<DateTimeOffset>)(((IProperty)createdAt).GetValueComparer())).Snapshot(source.GetCurrentValue<DateTimeOffset>(createdAt)), (source.GetCurrentValue<DateTimeOffset?>(editedAt) == null ? null : ((ValueComparer<DateTimeOffset?>)(((IProperty)editedAt).GetValueComparer())).Snapshot(source.GetCurrentValue<DateTimeOffset?>(editedAt))), ((ValueComparer<bool>)(((IProperty)isPinned).GetValueComparer())).Snapshot(source.GetCurrentValue<bool>(isPinned)), (source.GetCurrentValue<string>(text) == null ? null : ((ValueComparer<string>)(((IProperty)text).GetValueComparer())).Snapshot(source.GetCurrentValue<string>(text))), ((ValueComparer<ulong>)(((IProperty)userId).GetValueComparer())).Snapshot(source.GetCurrentValue<ulong>(userId)))));
                });
            runtimeEntityType.SetStoreGeneratedValuesFactory(
                ISnapshot () => Snapshot.Empty);
            runtimeEntityType.SetTemporaryValuesFactory(
                ISnapshot (InternalEntityEntry source) => Snapshot.Empty);
            runtimeEntityType.SetShadowValuesFactory(
                ISnapshot (IDictionary<string, object> source) => Snapshot.Empty);
            runtimeEntityType.SetEmptyShadowValuesFactory(
                ISnapshot () => Snapshot.Empty);
            runtimeEntityType.SetRelationshipSnapshotFactory(
                ISnapshot (InternalEntityEntry source) =>
                {
                    var entity = ((Message)(source.Entity));
                    return ((ISnapshot)(new Snapshot<ulong, object, object, object, object>(((ValueComparer<ulong>)(((IProperty)id).GetKeyValueComparer())).Snapshot(source.GetCurrentValue<ulong>(id)), MessageUnsafeAccessors.Embed(entity), MessageUnsafeAccessors.Mentions(entity), MessageUnsafeAccessors.Reference(entity), SnapshotFactoryFactory.SnapshotCollection(MessageUnsafeAccessors._attachments(entity)))));
                });
            runtimeEntityType.Counts = new PropertyCounts(
                propertyCount: 7,
                navigationCount: 4,
                complexPropertyCount: 0,
                originalValueCount: 7,
                shadowCount: 0,
                relationshipCount: 5,
                storeGeneratedCount: 0);
            runtimeEntityType.AddAnnotation("Mongo:CollectionName", "Messages");

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
