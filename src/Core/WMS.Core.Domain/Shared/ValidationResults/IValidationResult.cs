using WMS.Core.Domain.Shared.Errors;

namespace WMS.Core.Domain.Shared.ValidationResults;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        "ValidationError",
        "A validation problem occurred.");

    Error[] Errors { get; }
}