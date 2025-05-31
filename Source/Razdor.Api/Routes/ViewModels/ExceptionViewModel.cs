using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Api.Routes.ViewModels;

public record ExceptionViewModel(
    ErrorCode Code,
    string Message
);