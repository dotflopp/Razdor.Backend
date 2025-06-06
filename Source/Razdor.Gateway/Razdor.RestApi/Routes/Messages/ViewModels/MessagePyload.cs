using Razdor.Messages.Domain;
using Razdor.Messages.PublicEvents.ViewModels;

namespace Razdor.RestApi.Routes.Messages.ViewModels;

public record MessagePyload(
    Embed? Embed,
    string? Text,
    MessageReferenceViewModel? Reference
);