using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Razdor.Messaging.Domain;

public class Embed
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IReadOnlyCollection<EmbedField>? Fields { get; set; }
    public EmbedFooter? Footer { get; set; }
}

public class EmbedFooter
{
    public string? ImageUrl { get; set; }
    public string? Title { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
}

public class EmbedField
{
    public bool IsInline { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; } 
}