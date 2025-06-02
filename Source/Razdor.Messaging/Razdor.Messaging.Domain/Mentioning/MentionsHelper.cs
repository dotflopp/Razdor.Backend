using System.Text.RegularExpressions;

namespace Razdor.Messaging.Domain.Mentioning;

public static partial class MentionsHelper
{
    public static readonly Regex UserMentionRegex = GetUserMentionRegex();
    public static readonly Regex ChannelMentionRegex = GetChannelMentionRegex();
    public static readonly Regex RoleMentionRegex = GetRoleMentionRegex();
    public static readonly Regex EveryoneMentionRegex = GetEveryoneMentionRegex();
    
    [GeneratedRegex(@"@everyone")]
    public static partial Regex GetEveryoneMentionRegex();
    [GeneratedRegex(@"<@(\d+)>")]
    public static partial Regex GetUserMentionRegex();
    [GeneratedRegex(@"<#(\d+)(?:;(\d+))?>")]
    public static partial Regex GetChannelMentionRegex();
    [GeneratedRegex(@"<&(\d+)(?:;(\d+))?>")]
    public static partial Regex GetRoleMentionRegex();

    public static Mentions ExtractMentions(string? text = null, Embed? embed = null)
    {
        MentionsBuilder builder = new();

        if (text != null)
            FindMentions(text, builder);
        
        if (embed != null)
            FindEmbedMentions(embed, builder);
        
        return builder.Build();
    }
    
    private static void FindMentions(string text, MentionsBuilder builder)
    {
        FindChannelMentions(text, builder);
        FindUserMentions(text, builder);
        FindChannelMentions(text, builder);
        FindEveryoneMentions(text, builder);
    }

    private static void FindEmbedMentions(Embed embed, MentionsBuilder builder)
    {
        if (embed.Title != null)
            FindMentions(embed.Title, builder);
        
        if (embed.Description != null)
            FindMentions(embed.Description, builder);

        foreach (EmbedField field in embed.Fields ?? Enumerable.Empty<EmbedField>())
        {
            if (field.Title != null)
                FindMentions(field.Title, builder);
            if (field.Description != null)
                FindMentions(field.Description, builder);
        }
    }
    
    private static void FindEveryoneMentions(string text, MentionsBuilder builder)
    {
        builder.HasEveryoneMention(
            EveryoneMentionRegex.IsMatch(text)
        );
    }

    private static void FindChannelMentions(string text, MentionsBuilder builder)
    {
            
    }
    private static void FindUserMentions(string text, MentionsBuilder builder)
    {
        
    }
    
    private static void FindRoleMentions(string text, MentionsBuilder builder)
    {
        
    }


}