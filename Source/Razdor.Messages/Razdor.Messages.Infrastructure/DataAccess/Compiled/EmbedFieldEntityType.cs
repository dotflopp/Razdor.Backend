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
using Razdor.Messages.Domain;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Razdor.Messages.Infrastructure.DataAccess
{
    [EntityFrameworkInternal]
    public partial class EmbedFieldEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Razdor.Messages.Domain.EmbedField",
                typeof(EmbedField),
                baseEntityType,
                propertyCount: 6,
                foreignKeyCount: 1,
                unnamedIndexCount: 1,
                keyCount: 1);

            var embedMessageId = runtimeEntityType.AddProperty(
                "EmbedMessageId",
                typeof(ulong),
                afterSaveBehavior: PropertySaveBehavior.Throw,
                sentinel: 0ul);
            embedMessageId.SetAccessors(
                ulong (InternalEntityEntry entry) => entry.ReadShadowValue<ulong>(0),
                ulong (InternalEntityEntry entry) => entry.ReadShadowValue<ulong>(0),
                ulong (InternalEntityEntry entry) => entry.ReadOriginalValue<ulong>(embedMessageId, 0),
                ulong (InternalEntityEntry entry) => entry.ReadRelationshipSnapshotValue<ulong>(embedMessageId, 0),
                object (ValueBuffer valueBuffer) => valueBuffer[0]);
            embedMessageId.SetPropertyIndexes(
                index: 0,
                originalValueIndex: 0,
                shadowIndex: 0,
                relationshipIndex: 0,
                storeGenerationIndex: -1);
            embedMessageId.TypeMapping = MongoTypeMapping.Default.Clone(
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
            embedMessageId.SetCurrentValueComparer(new EntryCurrentValueComparer<ulong>(embedMessageId));

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(int),
                afterSaveBehavior: PropertySaveBehavior.Throw,
                sentinel: 0);
            id.SetAccessors(
                int (InternalEntityEntry entry) => entry.ReadShadowValue<int>(1),
                int (InternalEntityEntry entry) => entry.ReadShadowValue<int>(1),
                int (InternalEntityEntry entry) => entry.ReadOriginalValue<int>(id, 1),
                int (InternalEntityEntry entry) => entry.ReadRelationshipSnapshotValue<int>(id, 1),
                object (ValueBuffer valueBuffer) => valueBuffer[1]);
            id.SetPropertyIndexes(
                index: 1,
                originalValueIndex: 1,
                shadowIndex: 1,
                relationshipIndex: 1,
                storeGenerationIndex: -1);
            id.TypeMapping = MongoTypeMapping.Default.Clone(
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
            id.SetCurrentValueComparer(new EntryCurrentValueComparer<int>(id));

            var description = runtimeEntityType.AddProperty(
                "Description",
                typeof(string),
                propertyInfo: typeof(EmbedField).GetProperty("Description", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(EmbedField).GetField("<Description>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            description.SetGetter(
                string (EmbedField entity) => EmbedFieldUnsafeAccessors.Description(entity),
                bool (EmbedField entity) => EmbedFieldUnsafeAccessors.Description(entity) == null,
                string (EmbedField instance) => EmbedFieldUnsafeAccessors.Description(instance),
                bool (EmbedField instance) => EmbedFieldUnsafeAccessors.Description(instance) == null);
            description.SetSetter(
                (EmbedField entity, string value) => EmbedFieldUnsafeAccessors.Description(entity) = value);
            description.SetMaterializationSetter(
                (EmbedField entity, string value) => EmbedFieldUnsafeAccessors.Description(entity) = value);
            description.SetAccessors(
                string (InternalEntityEntry entry) => EmbedFieldUnsafeAccessors.Description(((EmbedField)(entry.Entity))),
                string (InternalEntityEntry entry) => EmbedFieldUnsafeAccessors.Description(((EmbedField)(entry.Entity))),
                string (InternalEntityEntry entry) => entry.ReadOriginalValue<string>(description, 2),
                string (InternalEntityEntry entry) => entry.GetCurrentValue<string>(description),
                object (ValueBuffer valueBuffer) => valueBuffer[2]);
            description.SetPropertyIndexes(
                index: 2,
                originalValueIndex: 2,
                shadowIndex: -1,
                relationshipIndex: -1,
                storeGenerationIndex: -1);
            description.TypeMapping = MongoTypeMapping.Default.Clone(
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

            var embedId = runtimeEntityType.AddProperty(
                "EmbedId",
                typeof(ulong),
                sentinel: 0ul);
            embedId.SetAccessors(
                ulong (InternalEntityEntry entry) => (entry.FlaggedAsStoreGenerated(3) ? entry.ReadStoreGeneratedValue<ulong>(0) : (entry.FlaggedAsTemporary(3) && entry.ReadShadowValue<ulong>(2) == 0UL ? entry.ReadTemporaryValue<ulong>(0) : entry.ReadShadowValue<ulong>(2))),
                ulong (InternalEntityEntry entry) => entry.ReadShadowValue<ulong>(2),
                ulong (InternalEntityEntry entry) => entry.ReadOriginalValue<ulong>(embedId, 3),
                ulong (InternalEntityEntry entry) => entry.ReadRelationshipSnapshotValue<ulong>(embedId, 2),
                object (ValueBuffer valueBuffer) => valueBuffer[3]);
            embedId.SetPropertyIndexes(
                index: 3,
                originalValueIndex: 3,
                shadowIndex: 2,
                relationshipIndex: 2,
                storeGenerationIndex: 0);
            embedId.TypeMapping = MongoTypeMapping.Default.Clone(
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
            embedId.SetCurrentValueComparer(new EntryCurrentValueComparer<ulong>(embedId));

            var isInline = runtimeEntityType.AddProperty(
                "IsInline",
                typeof(bool),
                propertyInfo: typeof(EmbedField).GetProperty("IsInline", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(EmbedField).GetField("<IsInline>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: false);
            isInline.SetGetter(
                bool (EmbedField entity) => EmbedFieldUnsafeAccessors.IsInline(entity),
                bool (EmbedField entity) => EmbedFieldUnsafeAccessors.IsInline(entity) == false,
                bool (EmbedField instance) => EmbedFieldUnsafeAccessors.IsInline(instance),
                bool (EmbedField instance) => EmbedFieldUnsafeAccessors.IsInline(instance) == false);
            isInline.SetSetter(
                (EmbedField entity, bool value) => EmbedFieldUnsafeAccessors.IsInline(entity) = value);
            isInline.SetMaterializationSetter(
                (EmbedField entity, bool value) => EmbedFieldUnsafeAccessors.IsInline(entity) = value);
            isInline.SetAccessors(
                bool (InternalEntityEntry entry) => EmbedFieldUnsafeAccessors.IsInline(((EmbedField)(entry.Entity))),
                bool (InternalEntityEntry entry) => EmbedFieldUnsafeAccessors.IsInline(((EmbedField)(entry.Entity))),
                bool (InternalEntityEntry entry) => entry.ReadOriginalValue<bool>(isInline, 4),
                bool (InternalEntityEntry entry) => entry.GetCurrentValue<bool>(isInline),
                object (ValueBuffer valueBuffer) => valueBuffer[4]);
            isInline.SetPropertyIndexes(
                index: 4,
                originalValueIndex: 4,
                shadowIndex: -1,
                relationshipIndex: -1,
                storeGenerationIndex: -1);
            isInline.TypeMapping = MongoTypeMapping.Default.Clone(
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

            var title = runtimeEntityType.AddProperty(
                "Title",
                typeof(string),
                propertyInfo: typeof(EmbedField).GetProperty("Title", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(EmbedField).GetField("<Title>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            title.SetGetter(
                string (EmbedField entity) => EmbedFieldUnsafeAccessors.Title(entity),
                bool (EmbedField entity) => EmbedFieldUnsafeAccessors.Title(entity) == null,
                string (EmbedField instance) => EmbedFieldUnsafeAccessors.Title(instance),
                bool (EmbedField instance) => EmbedFieldUnsafeAccessors.Title(instance) == null);
            title.SetSetter(
                (EmbedField entity, string value) => EmbedFieldUnsafeAccessors.Title(entity) = value);
            title.SetMaterializationSetter(
                (EmbedField entity, string value) => EmbedFieldUnsafeAccessors.Title(entity) = value);
            title.SetAccessors(
                string (InternalEntityEntry entry) => EmbedFieldUnsafeAccessors.Title(((EmbedField)(entry.Entity))),
                string (InternalEntityEntry entry) => EmbedFieldUnsafeAccessors.Title(((EmbedField)(entry.Entity))),
                string (InternalEntityEntry entry) => entry.ReadOriginalValue<string>(title, 5),
                string (InternalEntityEntry entry) => entry.GetCurrentValue<string>(title),
                object (ValueBuffer valueBuffer) => valueBuffer[5]);
            title.SetPropertyIndexes(
                index: 5,
                originalValueIndex: 5,
                shadowIndex: -1,
                relationshipIndex: -1,
                storeGenerationIndex: -1);
            title.TypeMapping = MongoTypeMapping.Default.Clone(
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

            var key = runtimeEntityType.AddKey(
                new[] { embedMessageId, id });
            runtimeEntityType.SetPrimaryKey(key);

            var index = runtimeEntityType.AddIndex(
                new[] { embedId });

            return runtimeEntityType;
        }

        public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("EmbedId") },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id") }),
                principalEntityType,
                deleteBehavior: DeleteBehavior.Cascade,
                required: true,
                ownership: true);

            var fields = principalEntityType.AddNavigation("Fields",
                runtimeForeignKey,
                onDependent: false,
                typeof(IReadOnlyCollection<EmbedField>),
                propertyInfo: typeof(Embed).GetProperty("Fields", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Embed).GetField("<Fields>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                eagerLoaded: true);

            fields.SetGetter(
                IReadOnlyCollection<EmbedField> (Embed entity) => EmbedUnsafeAccessors.Fields(entity),
                bool (Embed entity) => EmbedUnsafeAccessors.Fields(entity) == null,
                IReadOnlyCollection<EmbedField> (Embed instance) => EmbedUnsafeAccessors.Fields(instance),
                bool (Embed instance) => EmbedUnsafeAccessors.Fields(instance) == null);
            fields.SetSetter(
                (Embed entity, IReadOnlyCollection<EmbedField> value) => EmbedUnsafeAccessors.Fields(entity) = value);
            fields.SetMaterializationSetter(
                (Embed entity, IReadOnlyCollection<EmbedField> value) => EmbedUnsafeAccessors.Fields(entity) = value);
            fields.SetAccessors(
                IReadOnlyCollection<EmbedField> (InternalEntityEntry entry) => EmbedUnsafeAccessors.Fields(((Embed)(entry.Entity))),
                IReadOnlyCollection<EmbedField> (InternalEntityEntry entry) => EmbedUnsafeAccessors.Fields(((Embed)(entry.Entity))),
                null,
                IReadOnlyCollection<EmbedField> (InternalEntityEntry entry) => entry.GetCurrentValue<IReadOnlyCollection<EmbedField>>(fields),
                null);
            fields.SetPropertyIndexes(
                index: 0,
                originalValueIndex: -1,
                shadowIndex: -1,
                relationshipIndex: 1,
                storeGenerationIndex: -1);
            fields.SetCollectionAccessor<Embed, IReadOnlyCollection<EmbedField>, EmbedField>(
                IReadOnlyCollection<EmbedField> (Embed entity) => EmbedUnsafeAccessors.Fields(entity),
                (Embed entity, IReadOnlyCollection<EmbedField> collection) => EmbedUnsafeAccessors.Fields(entity) = ((IReadOnlyCollection<EmbedField>)(collection)),
                (Embed entity, IReadOnlyCollection<EmbedField> collection) => EmbedUnsafeAccessors.Fields(entity) = ((IReadOnlyCollection<EmbedField>)(collection)),
                IReadOnlyCollection<EmbedField> (Embed entity, Action<Embed, IReadOnlyCollection<EmbedField>> setter) => ClrCollectionAccessorFactory.CreateAndSetHashSet<Embed, IReadOnlyCollection<EmbedField>, EmbedField>(entity, setter),
                IReadOnlyCollection<EmbedField> () => ((IReadOnlyCollection<EmbedField>)(((ICollection<EmbedField>)(new HashSet<EmbedField>(ReferenceEqualityComparer.Instance))))));
            return runtimeForeignKey;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            var embedMessageId = runtimeEntityType.FindProperty("EmbedMessageId");
            var id = runtimeEntityType.FindProperty("Id");
            var description = runtimeEntityType.FindProperty("Description");
            var embedId = runtimeEntityType.FindProperty("EmbedId");
            var isInline = runtimeEntityType.FindProperty("IsInline");
            var title = runtimeEntityType.FindProperty("Title");
            var key = runtimeEntityType.FindKey(new[] { embedMessageId, id });
            key.SetPrincipalKeyValueFactory(KeyValueFactoryFactory.CreateCompositeFactory(key));
            key.SetIdentityMapFactory(IdentityMapFactoryFactory.CreateFactory<IReadOnlyList<object>>(key));
            runtimeEntityType.SetOriginalValuesFactory(
                ISnapshot (InternalEntityEntry source) =>
                {
                    var entity = ((EmbedField)(source.Entity));
                    return ((ISnapshot)(new Snapshot<ulong, int, string, ulong, bool, string>(((ValueComparer<ulong>)(((IProperty)embedMessageId).GetValueComparer())).Snapshot(source.GetCurrentValue<ulong>(embedMessageId)), ((ValueComparer<int>)(((IProperty)id).GetValueComparer())).Snapshot(source.GetCurrentValue<int>(id)), (source.GetCurrentValue<string>(description) == null ? null : ((ValueComparer<string>)(((IProperty)description).GetValueComparer())).Snapshot(source.GetCurrentValue<string>(description))), ((ValueComparer<ulong>)(((IProperty)embedId).GetValueComparer())).Snapshot(source.GetCurrentValue<ulong>(embedId)), ((ValueComparer<bool>)(((IProperty)isInline).GetValueComparer())).Snapshot(source.GetCurrentValue<bool>(isInline)), (source.GetCurrentValue<string>(title) == null ? null : ((ValueComparer<string>)(((IProperty)title).GetValueComparer())).Snapshot(source.GetCurrentValue<string>(title))))));
                });
            runtimeEntityType.SetStoreGeneratedValuesFactory(
                ISnapshot () => ((ISnapshot)(new Snapshot<ulong>(((ValueComparer<ulong>)(((IProperty)embedId).GetValueComparer())).Snapshot(default(ulong))))));
            runtimeEntityType.SetTemporaryValuesFactory(
                ISnapshot (InternalEntityEntry source) => ((ISnapshot)(new Snapshot<ulong>(default(ulong)))));
            runtimeEntityType.SetShadowValuesFactory(
                ISnapshot (IDictionary<string, object> source) => ((ISnapshot)(new Snapshot<ulong, int, ulong>((source.ContainsKey("EmbedMessageId") ? ((ulong)(source["EmbedMessageId"])) : 0UL), (source.ContainsKey("Id") ? ((int)(source["Id"])) : 0), (source.ContainsKey("EmbedId") ? ((ulong)(source["EmbedId"])) : 0UL)))));
            runtimeEntityType.SetEmptyShadowValuesFactory(
                ISnapshot () => ((ISnapshot)(new Snapshot<ulong, int, ulong>(default(ulong), default(int), default(ulong)))));
            runtimeEntityType.SetRelationshipSnapshotFactory(
                ISnapshot (InternalEntityEntry source) =>
                {
                    var entity = ((EmbedField)(source.Entity));
                    return ((ISnapshot)(new Snapshot<ulong, int, ulong>(((ValueComparer<ulong>)(((IProperty)embedMessageId).GetKeyValueComparer())).Snapshot(source.GetCurrentValue<ulong>(embedMessageId)), ((ValueComparer<int>)(((IProperty)id).GetKeyValueComparer())).Snapshot(source.GetCurrentValue<int>(id)), ((ValueComparer<ulong>)(((IProperty)embedId).GetKeyValueComparer())).Snapshot(source.GetCurrentValue<ulong>(embedId)))));
                });
            runtimeEntityType.Counts = new PropertyCounts(
                propertyCount: 6,
                navigationCount: 0,
                complexPropertyCount: 0,
                originalValueCount: 6,
                shadowCount: 3,
                relationshipCount: 3,
                storeGeneratedCount: 1);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
