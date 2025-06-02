using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Api.Middlewares.ViewModels;

public record ExceptionViewModel(
    ErrorCode Code,
    string Message
);