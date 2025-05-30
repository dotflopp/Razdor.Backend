﻿// <auto-generated />
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using MongoDB.EntityFrameworkCore.Storage;
using Razdor.Communities.Domain.Permissions;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Razdor.Communities.Infrastructure.DataAccess
{
    [EntityFrameworkInternal]
    public partial class OverwritePermissionsEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Razdor.Communities.Domain.Permissions.OverwritePermissions",
                typeof(OverwritePermissions),
                baseEntityType,
                propertyCount: 4,
                foreignKeyCount: 1,
                keyCount: 1);

            var overwritesPermissionChannelId = runtimeEntityType.AddProperty(
                "OverwritesPermissionChannelId",
                typeof(ulong),
                afterSaveBehavior: PropertySaveBehavior.Throw,
                sentinel: 0ul);
            overwritesPermissionChannelId.SetAccessors(
                ulong (InternalEntityEntry entry) => (entry.FlaggedAsStoreGenerated(0) ? entry.ReadStoreGeneratedValue<ulong>(0) : (entry.FlaggedAsTemporary(0) && entry.ReadShadowValue<ulong>(0) == 0UL ? entry.ReadTemporaryValue<ulong>(0) : entry.ReadShadowValue<ulong>(0))),
                ulong (InternalEntityEntry entry) => entry.ReadShadowValue<ulong>(0),
                ulong (InternalEntityEntry entry) => entry.ReadOriginalValue<ulong>(overwritesPermissionChannelId, 0),
                ulong (InternalEntityEntry entry) => entry.ReadRelationshipSnapshotValue<ulong>(overwritesPermissionChannelId, 0),
                object (ValueBuffer valueBuffer) => valueBuffer[0]);
            overwritesPermissionChannelId.SetPropertyIndexes(
                index: 0,
                originalValueIndex: 0,
                shadowIndex: 0,
                relationshipIndex: 0,
                storeGenerationIndex: 0);
            overwritesPermissionChannelId.TypeMapping = MongoTypeMapping.Default.Clone(
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
            overwritesPermissionChannelId.SetCurrentValueComparer(new EntryCurrentValueComparer<ulong>(overwritesPermissionChannelId));

            var overwriteId = runtimeEntityType.AddProperty(
                "OverwriteId",
                typeof(int),
                afterSaveBehavior: PropertySaveBehavior.Throw,
                sentinel: 0);
            overwriteId.SetAccessors(
                int (InternalEntityEntry entry) => (entry.FlaggedAsStoreGenerated(1) ? entry.ReadStoreGeneratedValue<int>(1) : (entry.FlaggedAsTemporary(1) && entry.ReadShadowValue<int>(1) == 0 ? entry.ReadTemporaryValue<int>(1) : entry.ReadShadowValue<int>(1))),
                int (InternalEntityEntry entry) => entry.ReadShadowValue<int>(1),
                int (InternalEntityEntry entry) => entry.ReadOriginalValue<int>(overwriteId, 1),
                int (InternalEntityEntry entry) => entry.ReadRelationshipSnapshotValue<int>(overwriteId, 1),
                object (ValueBuffer valueBuffer) => valueBuffer[1]);
            overwriteId.SetPropertyIndexes(
                index: 1,
                originalValueIndex: 1,
                shadowIndex: 1,
                relationshipIndex: 1,
                storeGenerationIndex: 1);
            overwriteId.TypeMapping = MongoTypeMapping.Default.Clone(
                comparer: new ValueComparer<int>(
                    bool (int v1, int v2) => v1 == v2,
                    int (int v) => v,
                    int (int v) => v),
                keyComparer: new ValueComparer<int>(
                    bool (int v1, int v2) => v1 == v2,
                    int (int v) => v,
                    int (int v) => v),
                providerValueComparer: new ValueComparer<int>(
                    bool (int v1, int v2) => v1 == v2,
                    int (int v) => v,
                    int (int v) => v),
                clrType: typeof(int));
            overwriteId.SetCurrentValueComparer(new EntryCurrentValueComparer<int>(overwriteId));

            var allow = runtimeEntityType.AddProperty(
                "Allow",
                typeof(UserPermissions),
                propertyInfo: typeof(OverwritePermissions).GetProperty("Allow", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(OverwritePermissions).GetField("<Allow>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: UserPermissions.None);
            allow.SetGetter(
                UserPermissions (OverwritePermissions entity) => OverwritePermissionsUnsafeAccessors.Allow(entity),
                bool (OverwritePermissions entity) => object.Equals(((object)(OverwritePermissionsUnsafeAccessors.Allow(entity))), ((object)(UserPermissions.None))),
                UserPermissions (OverwritePermissions instance) => OverwritePermissionsUnsafeAccessors.Allow(instance),
                bool (OverwritePermissions instance) => object.Equals(((object)(OverwritePermissionsUnsafeAccessors.Allow(instance))), ((object)(UserPermissions.None))));
            allow.SetSetter(
                (OverwritePermissions entity, UserPermissions value) => OverwritePermissionsUnsafeAccessors.Allow(entity) = value);
            allow.SetMaterializationSetter(
                (OverwritePermissions entity, UserPermissions value) => OverwritePermissionsUnsafeAccessors.Allow(entity) = value);
            allow.SetAccessors(
                UserPermissions (InternalEntityEntry entry) => OverwritePermissionsUnsafeAccessors.Allow(((OverwritePermissions)(entry.Entity))),
                UserPermissions (InternalEntityEntry entry) => OverwritePermissionsUnsafeAccessors.Allow(((OverwritePermissions)(entry.Entity))),
                UserPermissions (InternalEntityEntry entry) => entry.ReadOriginalValue<UserPermissions>(allow, 2),
                UserPermissions (InternalEntityEntry entry) => entry.GetCurrentValue<UserPermissions>(allow),
                object (ValueBuffer valueBuffer) => valueBuffer[2]);
            allow.SetPropertyIndexes(
                index: 2,
                originalValueIndex: 2,
                shadowIndex: -1,
                relationshipIndex: -1,
                storeGenerationIndex: -1);
            allow.TypeMapping = MongoTypeMapping.Default.Clone(
                comparer: new ValueComparer<UserPermissions>(
                    bool (UserPermissions v1, UserPermissions v2) => object.Equals(((object)(v1)), ((object)(v2))),
                    int (UserPermissions v) => ((object)v).GetHashCode(),
                    UserPermissions (UserPermissions v) => v),
                keyComparer: new ValueComparer<UserPermissions>(
                    bool (UserPermissions v1, UserPermissions v2) => object.Equals(((object)(v1)), ((object)(v2))),
                    int (UserPermissions v) => ((object)v).GetHashCode(),
                    UserPermissions (UserPermissions v) => v),
                providerValueComparer: new ValueComparer<UserPermissions>(
                    bool (UserPermissions v1, UserPermissions v2) => object.Equals(((object)(v1)), ((object)(v2))),
                    int (UserPermissions v) => ((object)v).GetHashCode(),
                    UserPermissions (UserPermissions v) => v),
                clrType: typeof(UserPermissions));

            var deny = runtimeEntityType.AddProperty(
                "Deny",
                typeof(UserPermissions),
                propertyInfo: typeof(OverwritePermissions).GetProperty("Deny", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(OverwritePermissions).GetField("<Deny>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: UserPermissions.None);
            deny.SetGetter(
                UserPermissions (OverwritePermissions entity) => OverwritePermissionsUnsafeAccessors.Deny(entity),
                bool (OverwritePermissions entity) => object.Equals(((object)(OverwritePermissionsUnsafeAccessors.Deny(entity))), ((object)(UserPermissions.None))),
                UserPermissions (OverwritePermissions instance) => OverwritePermissionsUnsafeAccessors.Deny(instance),
                bool (OverwritePermissions instance) => object.Equals(((object)(OverwritePermissionsUnsafeAccessors.Deny(instance))), ((object)(UserPermissions.None))));
            deny.SetSetter(
                (OverwritePermissions entity, UserPermissions value) => OverwritePermissionsUnsafeAccessors.Deny(entity) = value);
            deny.SetMaterializationSetter(
                (OverwritePermissions entity, UserPermissions value) => OverwritePermissionsUnsafeAccessors.Deny(entity) = value);
            deny.SetAccessors(
                UserPermissions (InternalEntityEntry entry) => OverwritePermissionsUnsafeAccessors.Deny(((OverwritePermissions)(entry.Entity))),
                UserPermissions (InternalEntityEntry entry) => OverwritePermissionsUnsafeAccessors.Deny(((OverwritePermissions)(entry.Entity))),
                UserPermissions (InternalEntityEntry entry) => entry.ReadOriginalValue<UserPermissions>(deny, 3),
                UserPermissions (InternalEntityEntry entry) => entry.GetCurrentValue<UserPermissions>(deny),
                object (ValueBuffer valueBuffer) => valueBuffer[3]);
            deny.SetPropertyIndexes(
                index: 3,
                originalValueIndex: 3,
                shadowIndex: -1,
                relationshipIndex: -1,
                storeGenerationIndex: -1);
            deny.TypeMapping = MongoTypeMapping.Default.Clone(
                comparer: new ValueComparer<UserPermissions>(
                    bool (UserPermissions v1, UserPermissions v2) => object.Equals(((object)(v1)), ((object)(v2))),
                    int (UserPermissions v) => ((object)v).GetHashCode(),
                    UserPermissions (UserPermissions v) => v),
                keyComparer: new ValueComparer<UserPermissions>(
                    bool (UserPermissions v1, UserPermissions v2) => object.Equals(((object)(v1)), ((object)(v2))),
                    int (UserPermissions v) => ((object)v).GetHashCode(),
                    UserPermissions (UserPermissions v) => v),
                providerValueComparer: new ValueComparer<UserPermissions>(
                    bool (UserPermissions v1, UserPermissions v2) => object.Equals(((object)(v1)), ((object)(v2))),
                    int (UserPermissions v) => ((object)v).GetHashCode(),
                    UserPermissions (UserPermissions v) => v),
                clrType: typeof(UserPermissions));

            var key = runtimeEntityType.AddKey(
                new[] { overwritesPermissionChannelId, overwriteId });
            runtimeEntityType.SetPrimaryKey(key);

            return runtimeEntityType;
        }

        public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("OverwritesPermissionChannelId"), declaringEntityType.FindProperty("OverwriteId") },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("OverwritesPermissionChannelId"), principalEntityType.FindProperty("Id") }),
                principalEntityType,
                deleteBehavior: DeleteBehavior.Cascade,
                unique: true,
                required: true,
                requiredDependent: true,
                ownership: true);

            var permissions = principalEntityType.AddNavigation("Permissions",
                runtimeForeignKey,
                onDependent: false,
                typeof(OverwritePermissions),
                propertyInfo: typeof(Overwrite).GetProperty("Permissions", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Overwrite).GetField("<Permissions>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                eagerLoaded: true);

            permissions.SetGetter(
                OverwritePermissions (Overwrite entity) => OverwriteUnsafeAccessors.Permissions(entity),
                bool (Overwrite entity) => OverwriteUnsafeAccessors.Permissions(entity) == null,
                OverwritePermissions (Overwrite instance) => OverwriteUnsafeAccessors.Permissions(instance),
                bool (Overwrite instance) => OverwriteUnsafeAccessors.Permissions(instance) == null);
            permissions.SetSetter(
                (Overwrite entity, OverwritePermissions value) => OverwriteUnsafeAccessors.Permissions(entity) = value);
            permissions.SetMaterializationSetter(
                (Overwrite entity, OverwritePermissions value) => OverwriteUnsafeAccessors.Permissions(entity) = value);
            permissions.SetAccessors(
                OverwritePermissions (InternalEntityEntry entry) => OverwriteUnsafeAccessors.Permissions(((Overwrite)(entry.Entity))),
                OverwritePermissions (InternalEntityEntry entry) => OverwriteUnsafeAccessors.Permissions(((Overwrite)(entry.Entity))),
                null,
                OverwritePermissions (InternalEntityEntry entry) => entry.GetCurrentValue<OverwritePermissions>(permissions),
                null);
            permissions.SetPropertyIndexes(
                index: 0,
                originalValueIndex: -1,
                shadowIndex: -1,
                relationshipIndex: 2,
                storeGenerationIndex: -1);
            return runtimeForeignKey;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            var overwritesPermissionChannelId = runtimeEntityType.FindProperty("OverwritesPermissionChannelId");
            var overwriteId = runtimeEntityType.FindProperty("OverwriteId");
            var allow = runtimeEntityType.FindProperty("Allow");
            var deny = runtimeEntityType.FindProperty("Deny");
            var key = runtimeEntityType.FindKey(new[] { overwritesPermissionChannelId, overwriteId });
            key.SetPrincipalKeyValueFactory(KeyValueFactoryFactory.CreateCompositeFactory(key));
            key.SetIdentityMapFactory(IdentityMapFactoryFactory.CreateFactory<IReadOnlyList<object>>(key));
            runtimeEntityType.SetOriginalValuesFactory(
                ISnapshot (InternalEntityEntry source) =>
                {
                    var entity = ((OverwritePermissions)(source.Entity));
                    return ((ISnapshot)(new Snapshot<ulong, int, UserPermissions, UserPermissions>(((ValueComparer<ulong>)(((IProperty)overwritesPermissionChannelId).GetValueComparer())).Snapshot(source.GetCurrentValue<ulong>(overwritesPermissionChannelId)), ((ValueComparer<int>)(((IProperty)overwriteId).GetValueComparer())).Snapshot(source.GetCurrentValue<int>(overwriteId)), ((ValueComparer<UserPermissions>)(((IProperty)allow).GetValueComparer())).Snapshot(source.GetCurrentValue<UserPermissions>(allow)), ((ValueComparer<UserPermissions>)(((IProperty)deny).GetValueComparer())).Snapshot(source.GetCurrentValue<UserPermissions>(deny)))));
                });
            runtimeEntityType.SetStoreGeneratedValuesFactory(
                ISnapshot () => ((ISnapshot)(new Snapshot<ulong, int>(((ValueComparer<ulong>)(((IProperty)overwritesPermissionChannelId).GetValueComparer())).Snapshot(default(ulong)), ((ValueComparer<int>)(((IProperty)overwriteId).GetValueComparer())).Snapshot(default(int))))));
            runtimeEntityType.SetTemporaryValuesFactory(
                ISnapshot (InternalEntityEntry source) => ((ISnapshot)(new Snapshot<ulong, int>(default(ulong), default(int)))));
            runtimeEntityType.SetShadowValuesFactory(
                ISnapshot (IDictionary<string, object> source) => ((ISnapshot)(new Snapshot<ulong, int>((source.ContainsKey("OverwritesPermissionChannelId") ? ((ulong)(source["OverwritesPermissionChannelId"])) : 0UL), (source.ContainsKey("OverwriteId") ? ((int)(source["OverwriteId"])) : 0)))));
            runtimeEntityType.SetEmptyShadowValuesFactory(
                ISnapshot () => ((ISnapshot)(new Snapshot<ulong, int>(default(ulong), default(int)))));
            runtimeEntityType.SetRelationshipSnapshotFactory(
                ISnapshot (InternalEntityEntry source) =>
                {
                    var entity = ((OverwritePermissions)(source.Entity));
                    return ((ISnapshot)(new Snapshot<ulong, int>(((ValueComparer<ulong>)(((IProperty)overwritesPermissionChannelId).GetKeyValueComparer())).Snapshot(source.GetCurrentValue<ulong>(overwritesPermissionChannelId)), ((ValueComparer<int>)(((IProperty)overwriteId).GetKeyValueComparer())).Snapshot(source.GetCurrentValue<int>(overwriteId)))));
                });
            runtimeEntityType.Counts = new PropertyCounts(
                propertyCount: 4,
                navigationCount: 0,
                complexPropertyCount: 0,
                originalValueCount: 4,
                shadowCount: 2,
                relationshipCount: 2,
                storeGeneratedCount: 2);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
