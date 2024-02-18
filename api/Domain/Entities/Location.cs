using Domain.Common;

namespace Domain.Entities;

public class Location : BaseAuditableEntity
{
    public string Name { get; set; }
    public string City { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public Guid UserId { get; set; }
}
