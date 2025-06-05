using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Api.ExceptionHandleMiddlewares.ViewModels;

public record ExceptionViewModel(
    ErrorCode Code,
    string Message
);