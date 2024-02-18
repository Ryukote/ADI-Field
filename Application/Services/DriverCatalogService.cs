using System.Security.Authentication;
using Application.Common;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class DriverCatalogService
{
    private readonly ADIFieldDbContext _dbContext;
    private readonly IMapper _mapper;
    public DriverCatalogService(ADIFieldDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<DriverCatalogDTO> GetByIdAsync(Guid id)
    {
        try
        {
            var driverCatalog = await _dbContext.DriverCatalog
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (driverCatalog != null)
            {
                return _mapper.Map<DriverCatalogDTO>(driverCatalog);
            }

            throw new KeyNotFoundException($"Could not find driver catalog with id={id}. Possible missing id or driver is soft deleted in the catalog.");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Paginated<DriverCatalogDTO>> GetPaginatedAsync(int skip, int take)
    {
        try
        {
            var driverCatalog = _dbContext.DriverCatalog
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Skip(skip)
                .Take(take);

            var total = await _dbContext.DriverCatalog
                .Where(x => !x.IsDeleted)
                .CountAsync();

            if (driverCatalog != null)
            {
                return new Paginated<DriverCatalogDTO>()
                {
                    Result = await driverCatalog
                        .ProjectTo<DriverCatalogDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync(),
                    Skip = skip,
                    Take = take,
                    Total = total
                };
            }

            throw new KeyNotFoundException("Something went wrong with getting pagination result for driver catalog. Please check database connection.");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Guid> CreateAsync(CreateDriverCatalogDTO driverCatalog)
    {
        try
        {
            var driverCatalogData = _mapper.Map<DriverCatalog>(driverCatalog);

            driverCatalogData.CreatedById = driverCatalog.UserId;
            driverCatalogData.Created = DateTime.UtcNow;

            await _dbContext.DriverCatalog.AddAsync(driverCatalogData);

            await _dbContext.SaveChangesAsync();

            return driverCatalogData.Id;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<int> UpdateAsync(Guid id, UpdateDriverCatalogDTO driverCatalog, Guid userId)
    {
        try
        {
            var result = await _dbContext.DriverCatalog
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                throw new Exception($"Could not find driver catalog with id={id}. Possible missing id.");
            }

            if (result.UserId != userId)
            {
                throw new AuthenticationException($"User with id={userId} is not authorized to update driver catalog with id={id}.");
            }

            result.Name = driverCatalog.Name;
            result.CarManufacturer = driverCatalog.CarManufacturer;
            result.NumberOfPassangers = driverCatalog.NumberOfPassangers;
            result.CarModel = driverCatalog.CarModel;
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
            var driverCatalog = await _dbContext.DriverCatalog.FindAsync(id);

            if (driverCatalog?.UserId == userId)
            {
                driverCatalog.IsDeleted = true;

                //_dbContext.DriverCatalog.Remove(driverCatalog);

                return await _dbContext.SaveChangesAsync();
            }

            throw new KeyNotFoundException($"Could not delete driver catalog with id={id}. Possible missing id or request is not from creator.");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
