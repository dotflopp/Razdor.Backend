using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Messages.Domain;
using Razdor.Messages.Domain.Mentioning;
using Razdor.Messages.Module.DataAccess;

namespace Razdor.Messages.Infrastructure.DataAccess;

public class MessagesMongoDbContext(
    DbContextOptions<MessagesMongoDbContext> options
) : MessagesDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         EntityTypeBuilder<Message> builder = modelBuilder.Entity<Message>();
         
         builder.HasKey(m => m.Id);
         builder.OwnsOne(x => x.Embed, ownsBuilder =>
         {
             ownsBuilder.WithOwner().HasForeignKey(nameof(Message.Id));
             ownsBuilder.HasKey(nameof(Message.Id));
             
             ownsBuilder.OwnsOne(x => x.Footer, footerBuilder =>
             {
                 ownsBuilder.WithOwner().HasForeignKey(nameof(Message.Id));
                 ownsBuilder.HasKey(nameof(Message.Id));
             });
             
             ownsBuilder.OwnsMany(x => x.Fields);
         });

         builder.OwnsOne(x => x.Reference, ownsBuilder =>
         {
             ownsBuilder.WithOwner().HasForeignKey(nameof(Message.Id));
             ownsBuilder.HasKey(nameof(Message.Id));
         });
         
         builder.Ignore(x => x.MentionedChannels);
         builder.Ignore(x => x.MentionedUsers);
         builder.Ignore(x => x.MentionedRoles);
         
         builder.OwnsOne(x => x.Mentions, ownsBuilder =>
         {
             ownsBuilder.WithOwner().HasForeignKey(nameof(Message.Id));
             ownsBuilder.HasKey(nameof(Message.Id));
             
             ownsBuilder.Ignore(x => x.Channels);
             ownsBuilder.Ignore(x => x.Users);
             ownsBuilder.Ignore(x => x.Roles);
             
             ownsBuilder.OwnsMany<MentionedChannel>("_channels")
                 .HasElementName(nameof(Mentions.Channels));
             ownsBuilder.OwnsMany<MentionedUser>("_users")
                 .HasElementName(nameof(Mentions.Users));
             ownsBuilder.OwnsMany<MentionedRole>("_roles")
                 .HasElementName(nameof(Mentions.Roles));
         });

         builder.Ignore(x => x.Attachments);
         builder.OwnsMany<Attachment>("_attachments")
             .HasElementName(nameof(Message.Attachments));
    }
}