using Application.Common;

namespace Application.DTOs;

public class DriverCatalogDTO : IGetDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string CarManufacturer { get; set; }
    public required string CarModel { get; set; }
    public int NumberOfPassangers { get; set; }
}
