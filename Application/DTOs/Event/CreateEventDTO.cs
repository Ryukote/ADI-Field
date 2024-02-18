using Domain.Entities;

namespace Application.DTOs.Event
{
    public class CreateEventDTO
    {
        public required string Name { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; } = default!;
        public EventType EventType { get; set; }
        public DateTime EventStartsAt { get; set; }
        public DateTime? EventEndsAt { get; set; }
        public EventAccomodationType EventAccomodationTypes { get; set; }
        public bool IsMoreInfoAvailable { get; set; }
        public string EventDetailsLink { get; set; } = default!;
    }
}
