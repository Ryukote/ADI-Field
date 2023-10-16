using Domain.Common;

namespace Api.Domain.Entities;

public class Calendar : BaseAuditableEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    // public ICollection<CalendarEvent> CalendarEvents { get; set; } = new List<CalendarEvent>();
}
