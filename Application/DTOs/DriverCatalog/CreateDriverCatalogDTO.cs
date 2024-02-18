namespace Application.DTOs;

public class CreateDriverCatalogDTO
{
    public required string Name { get; set; }
    //UserId of the driver
    public Guid UserId { get; set; }
    public required string CarManufacturer { get; set; }
    public required string CarModel { get; set; }
    public required int NumberOfPassangers { get; set; }
}
