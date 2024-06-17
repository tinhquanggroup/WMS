using WMS.Core.Domain.Entities;

namespace WMS.Core.Application.Contracts.Responses.Locations;

public record LocationResponse
{
    public int LocationId { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string PointX { get; set; } = null!;
    public string PointY { get; set; } = null!;
    public string PointZ { get; set; } = null!;
    public Zone Zone { get; set; } = null!;
}