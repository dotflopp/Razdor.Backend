using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Messaging.Domain;
using Razdor.Messaging.Domain.Mentioning;
using Razdor.Messaging.Module.DataAccess;

namespace Razdor.Messaging.Infrastructure.DataAccess;

public class MessagingMongoDBContext(
    DbContextOptions<MessagingMongoDBContext> options
) : MessagingDataContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         EntityTypeBuilder<Message> builder = modelBuilder.Entity<Message>();
         
         builder.HasKey(m => m.Id);
         builder.OwnsOne(x => x.Embed, ownsBuilder =>
         {
             ownsBuilder.OwnsOne(x => x.Footer);
             ownsBuilder.OwnsMany(x => x.Fields);
         });

         builder.OwnsOne(x => x.Reference);
         builder.OwnsOne(x => x.Mentions, ownsBuilder =>
         {
             ownsBuilder.OwnsMany<MentionedChannel>("_channels")
                 .HasElementName(nameof(Mentions.Channels));
             ownsBuilder.OwnsMany<MentionedUser>("_users")
                 .HasElementName(nameof(Mentions.Users));
             ownsBuilder.OwnsMany<MentionedRole>("_roles")
                 .HasElementName(nameof(Mentions.Roles));
         });
         
         builder.OwnsMany<Attachment>("_attachments")
             .HasElementName(nameof(Message.Attachments));
    }
}