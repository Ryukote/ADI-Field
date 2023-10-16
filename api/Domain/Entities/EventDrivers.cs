using Domain.Common;

namespace Domain.Entities;

public class EventDrivers : BaseAuditableEntity
{
    public Guid EventId { get; set; }
    public Guid DriverCatalogId { get; set; }

    public int NumberOfSeatsRemaining { get; set; }
    public bool IsSpaceForMoreEquipmentAvailable { get; set; }
    public string EquipmentSpaceDescription { get; set; }

    public virtual Event Event { get; set; }
    public virtual DriverCatalog DriverCatalog { get; set; }
}
