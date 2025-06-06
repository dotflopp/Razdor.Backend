﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Razdor.Communities.Infrastructure.DataAccess
{
    public partial class CommunitiesMongoDbContextModel
    {
        private CommunitiesMongoDbContextModel()
            : base(skipDetectChanges: false, modelId: new Guid("a4603e90-b421-4fb5-84cc-5ea232982c6d"), entityTypeCount: 15)
        {
        }

        partial void Initialize()
        {
            var communityChannel = CommunityChannelEntityType.Create(this);
            var overwrite = OverwriteEntityType.Create(this);
            var community = CommunityEntityType.Create(this);
            var invite = InviteEntityType.Create(this);
            var communityMember = CommunityMemberEntityType.Create(this);
            var voiceState = VoiceStateEntityType.Create(this);
            var everyonePermissions = EveryonePermissionsEntityType.Create(this);
            var overwritePermissions = OverwritePermissionsEntityType.Create(this);
            var role = RoleEntityType.Create(this);
            var mediaFileMeta = MediaFileMetaEntityType.Create(this);
            var forkChannel = ForkChannelEntityType.Create(this, communityChannel);
            var overwritesPermissionChannel = OverwritesPermissionChannelEntityType.Create(this, communityChannel);
            var categoryChannel = CategoryChannelEntityType.Create(this, overwritesPermissionChannel);
            var textChannel = TextChannelEntityType.Create(this, overwritesPermissionChannel);
            var voiceChannel = VoiceChannelEntityType.Create(this, overwritesPermissionChannel);

            OverwriteEntityType.CreateForeignKey1(overwrite, overwritesPermissionChannel);
            VoiceStateEntityType.CreateForeignKey1(voiceState, communityMember);
            EveryonePermissionsEntityType.CreateForeignKey1(everyonePermissions, community);
            OverwritePermissionsEntityType.CreateForeignKey1(overwritePermissions, overwrite);
            RoleEntityType.CreateForeignKey1(role, community);
            MediaFileMetaEntityType.CreateForeignKey1(mediaFileMeta, community);

            CommunityChannelEntityType.CreateAnnotations(communityChannel);
            OverwriteEntityType.CreateAnnotations(overwrite);
            CommunityEntityType.CreateAnnotations(community);
            InviteEntityType.CreateAnnotations(invite);
            CommunityMemberEntityType.CreateAnnotations(communityMember);
            VoiceStateEntityType.CreateAnnotations(voiceState);
            EveryonePermissionsEntityType.CreateAnnotations(everyonePermissions);
            OverwritePermissionsEntityType.CreateAnnotations(overwritePermissions);
            RoleEntityType.CreateAnnotations(role);
            MediaFileMetaEntityType.CreateAnnotations(mediaFileMeta);
            ForkChannelEntityType.CreateAnnotations(forkChannel);
            OverwritesPermissionChannelEntityType.CreateAnnotations(overwritesPermissionChannel);
            CategoryChannelEntityType.CreateAnnotations(categoryChannel);
            TextChannelEntityType.CreateAnnotations(textChannel);
            VoiceChannelEntityType.CreateAnnotations(voiceChannel);

            AddAnnotation("ProductVersion", "9.0.5");
        }
    }
}
