using Razdor.Messages.Domain;
using Razdor.Messages.Module.Services.Commands.ViewModels;

namespace Razdor.Api.Routes.Messaging.ViewModels;

public record MessagePyload(
    Embed? Embed,
    string? Text,
    MessageReferenceViewModel? Reference
);