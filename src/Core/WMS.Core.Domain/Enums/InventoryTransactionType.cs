using System.ComponentModel;

namespace WMS.Core.Domain.Enums;

public enum InventoryTransactionType
{
    [Description("In")]
    In = 0,
    [Description("Out")]
    Out = 1,
    [Description("Transfer")]
    Transfer = 2
}