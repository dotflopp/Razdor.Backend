using Razdor.Messaging.Domain;
using Razdor.Messaging.Module.Services.Commands.ViewModels;

namespace Razdor.Api.Routes.Messaging.ViewModels;

public record MessagePyload(
    Embed? Embed,
    string? Text,
    MessageReferenceViewModel? Reference
);