using Razdor.Messages.Domain;
using Razdor.Messages.Module.Services.Commands.ViewModels;

namespace Razdor.RestApi.Routes.Messages.ViewModels;

public record MessagePyload(
    Embed? Embed,
    string? Text,
    MessageReferenceViewModel? Reference
);