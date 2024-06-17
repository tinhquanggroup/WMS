using System.ComponentModel.DataAnnotations.Schema;
using WMS.Core.Domain.Abstractions;
using WMS.Core.Domain.Utils;

namespace WMS.Core.Domain.Entities;

[Table(name: "Zones")]
public class Zone : BaseEntity
{
    public string Name { get; private set; } = null!;

    [InverseProperty(nameof(Zone))]
    public virtual ICollection<Location> Locations { get; set; } = [];

    private Zone() { }

    public static Zone Create(string name)
    {
        var zone = new Zone
        {
            Name = name,
            IsActive = true,
            CreatedAt = TimeHelper.GetCurrentTime(),
            UpdatedAt = TimeHelper.GetCurrentTime(),
            IsDeleted = false
        };

        return zone;
    }
}