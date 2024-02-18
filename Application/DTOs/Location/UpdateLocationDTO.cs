namespace Application.DTOs.Location
{
    public class UpdateLocationDTO
    {
        public required string Name { get; set; }
        public string City { get; set; } = default!;
        public required decimal Latitude { get; set; }
        public required decimal Longitude { get; set; }
        public Guid UserId { get; set; }
    }
}
