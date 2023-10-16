using Domain.Common;

namespace Domain.Entities;

public class Event : BaseAuditableEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public EventType EventType { get; set; }
    public DateTime EventStartsAt { get; set; }
    public DateTime? EventEndsAt { get; set; }
    public string EventAccomodationTypes { get; set; }
    public bool IsMoreInfoAvailable { get; set; }
    public string EventDetailsLink { get; set; }
}
