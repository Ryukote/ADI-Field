using Domain.Common;

namespace Domain.Entities;

public class Event : BaseAuditableEntity
{
    public required string Name { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; } = default!;
    public EventType EventType { get; set; }
    public DateTime EventStartsAt { get; set; }
    public DateTime? EventEndsAt { get; set; }
    public EventAccomodationType EventAccomodationType { get; set; }
    public bool IsMoreInfoAvailable { get; set; }
    public string EventDetailsLink { get; set; } = default!;

    public virtual User User { get; set; }
}
