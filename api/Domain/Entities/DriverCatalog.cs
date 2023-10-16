using Domain.Common;

namespace Domain.Entities;

public class DriverCatalog : BaseAuditableEntity
{
    //Full name of the driver
    public string Name { get; set; }
    //UserId of the driver
    public Guid UserId { get; set; }
    public string CarManufacturer { get; set; }
    public string CarModel { get; set; }
    public int NumberOfPassangers { get; set; }

    public virtual User User { get; set; }
}
