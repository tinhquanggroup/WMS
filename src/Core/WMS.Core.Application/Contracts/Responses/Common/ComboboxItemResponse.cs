namespace WMS.Core.Application.Contracts.Responses.Common;

public sealed record ComboboxItemResponse
{
    public required object Key { get; set; }
    public required object Val { get; set; }
    public object AdditionalValue { get; set; } = string.Empty;
};