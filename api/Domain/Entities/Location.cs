using Domain.Common;

namespace Domain.Entities;

public class Location : BaseAuditableEntity
{
    public string Name { get; set; }
    public string City { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}
