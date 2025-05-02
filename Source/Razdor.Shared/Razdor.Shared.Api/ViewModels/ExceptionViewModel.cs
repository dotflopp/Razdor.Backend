using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Api.ViewModels;

public record ExceptionViewModel(
  ErrorCodes Code,
  string Message
);