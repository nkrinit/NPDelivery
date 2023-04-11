using Remora.Results;

namespace NPDelivery.Errors;

public record ValidationError(
    //ReadOnlyList<ValidationFailure> ValidationFailures,
    string Message = "One or more properties did not validate successfully."
) : ResultError(Message);
