using Razdor.Shared.Domain.Exceptions;

namespace Razdor.RestApi.ExceptionHandleMiddlewares.ViewModels;

public record ExceptionViewModel(
    ErrorCode Code,
    string Message
);