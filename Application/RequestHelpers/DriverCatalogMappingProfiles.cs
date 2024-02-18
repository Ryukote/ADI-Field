using AutoMapper;
using Application.DTOs;
using Domain.Entities;

namespace Application;

public class DriverCatalogMappingProfiles : Profile
{
    public DriverCatalogMappingProfiles()
    {
        CreateMap<CreateDriverCatalogDTO, DriverCatalog>();
        CreateMap<DriverCatalog, DriverCatalogDTO>();
    }
}
