namespace Application.DTOs.Location
{
    public class CreateLocationDTO
    {
        public required string Name { get; set; }
        public string City { get; set; } = default!;
        public required decimal Latitude { get; set; }
        public required decimal Longitude { get; set; }
        public required Guid UserId { get; set; }
    }
}
