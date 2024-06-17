using WMS.Core.Domain.Shared.Errors;
using WMS.Core.Domain.Shared.Results;

namespace WMS.Core.Domain.Shared.ValidationResults;

public sealed class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(false, IValidationResult.ValidationError) =>
        Errors = errors;

    public Error[] Errors { get; }

    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}