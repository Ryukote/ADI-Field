namespace Application.DTOs;

public class UpdateDriverCatalogDTO
{
    public required string Name { get; set; }
    //UserId of the driver
    public Guid UserId { get; set; }
    public required string CarManufacturer { get; set; }
    public required string CarModel { get; set; }
    public int NumberOfPassangers { get; set; }
}
