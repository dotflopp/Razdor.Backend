using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Api.ViewModels;

public record ExceptionViewModel(
  ErrorCode Code,
  string Message
);