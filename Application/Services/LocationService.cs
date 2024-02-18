using Application.Common;
using Application.DTOs.Event;
using Application.DTOs.Location;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace Application.Services
{
    public class LocationService
    {
        private readonly ADIFieldDbContext _dbContext;
        private readonly IMapper _mapper;

        public LocationService(ADIFieldDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<LocationDTO> GetByIdAsync(Guid id)
        {
            try
            {
                var locationData = await _dbContext.Location
                    .Where(x => !x.IsDeleted)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (locationData != null)
                {
                    return _mapper.Map<LocationDTO>(locationData);
                }

                throw new KeyNotFoundException($"Could not find event with id={id}. Possible missing id or location is soft deleted.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Paginated<LocationDTO>> GetPaginatedAsync(int skip, int take)
        {
            try
            {
                var locationData = _dbContext.Location
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted)
                    .Skip(skip)
                    .Take(take);

                var total = await _dbContext.Location
                    .Where(x => !x.IsDeleted)
                    .CountAsync();

                if (locationData != null)
                {
                    return new Paginated<LocationDTO>()
                    {
                        Result = await locationData
                            .ProjectTo<LocationDTO>(_mapper.ConfigurationProvider)
                            .ToListAsync(),
                        Skip = skip,
                        Take = take,
                        Total = total
                    };
                }

                throw new KeyNotFoundException("Something went wrong with getting pagination result for location. Please check database connection.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Guid> CreateAsync(CreateLocationDTO locationDTO)
        {
            try
            {
                var locationData = _mapper.Map<Location>(locationDTO);

                locationData.CreatedById = locationDTO.UserId;
                locationData.Created = DateTime.UtcNow;

                await _dbContext.Location.AddAsync(locationData);

                await _dbContext.SaveChangesAsync();

                return locationData.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(Guid id, UpdateLocationDTO locationDTO, Guid userId)
        {
            try
            {
                var result = await _dbContext.Location
                    .Where(x => !x.IsDeleted)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (result == null)
                {
                    throw new Exception($"Could not find location with id={id}. Possible missing id or location is soft deleted.");
                }

                if (result.UserId != userId)
                {
                    throw new AuthenticationException($"User with id={userId} is not authorized to update location with id={id}.");
                }

                result.Name = locationDTO.Name;
                result.City = locationDTO.City;
                result.Latitude = locationDTO.Latitude;
                result.Longitude = locationDTO.Longitude;
                result.LastModifiedById = userId;
                result.LastModified = DateTime.UtcNow;

                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteAsync(Guid id, Guid userId)
        {
            try
            {
                var location = await _dbContext.Location.FindAsync(id);

                if (location?.UserId == userId)
                {
                    location.IsDeleted = true;

                    //_dbContext.DriverCatalog.Remove(driverCatalog);

                    return await _dbContext.SaveChangesAsync();
                }

                throw new KeyNotFoundException($"Could not delete location with id={id}. Possible missing id or request is not from creator.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
