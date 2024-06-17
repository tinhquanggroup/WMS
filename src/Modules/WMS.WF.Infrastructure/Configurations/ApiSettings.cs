namespace WMS.WF.Infrastructure.Configurations;

public record ApiSettings
{
    public ApiSettings(){}

    public string BaseUrl { get; set; }
    public Endpoints Endpoints { get; set; }
}