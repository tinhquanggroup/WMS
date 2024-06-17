using System.ComponentModel.DataAnnotations.Schema;
using WMS.Core.Domain.Abstractions;
using WMS.Core.Domain.Utils;

namespace WMS.Core.Domain.Entities;

[Table(name: "Locations")]
public class Location : BaseEntity
{
    public string Code { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public int ZoneId { get; private set; }
    public int Capacity { get; private set; }
    public string PointX { get; private set; } = null!;
    public string PointY { get; private set; } = null!;
    public string PointZ { get; private set; } = null!;

    [ForeignKey(nameof(ZoneId))]
    public virtual Zone Zone { get; set; } = null!;

    [InverseProperty(nameof(Location))]
    public virtual ICollection<Inventory> Inventories { get; set; } = [];

    private Location() { }

    public static Location Create(
        string code,
        string name,
        int zoneId,
        int capacity,
        string pointX,
        string pointY,
        string pointZ)
    {
        var location = new Location
        {
            Code = code,
            Name = name,
            ZoneId = zoneId,
            Capacity = capacity,
            PointX = pointX,
            PointY = pointY,
            PointZ = pointZ,
            IsActive = true,
            CreatedAt = TimeHelper.GetCurrentTime(),
            UpdatedAt = TimeHelper.GetCurrentTime(),
            IsDeleted = false
        };

        return location;
    }
}