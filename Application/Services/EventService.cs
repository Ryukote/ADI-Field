using Application.Common;
using Application.DTOs;
using Application.DTOs.Event;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace Application;

public class EventService
{
    private readonly ADIFieldDbContext _dbContext;
    private readonly IMapper _mapper;

    public EventService(ADIFieldDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<EventDTO> GetByIdAsync(Guid id)
    {
        try
        {
            var eventData = await _dbContext.Event
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (eventData != null)
            {
                return _mapper.Map<EventDTO>(eventData);
            }

            throw new KeyNotFoundException($"Could not find event with id={id}. Possible missing id or event is soft deleted.");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Paginated<EventDTO>> GetPaginatedAsync(int skip, int take)
    {
        try
        {
            var eventData = _dbContext.Event
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Skip(skip)
                .Take(take);

            var total = await _dbContext.Event
                .Where(x => !x.IsDeleted)
                .CountAsync();

            if (eventData != null)
            {
                return new Paginated<EventDTO>()
                {
                    Result = await eventData
                        .ProjectTo<EventDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync(),
                    Skip = skip,
                    Take = take,
                    Total = total
                };
            }

            throw new KeyNotFoundException("Something went wrong with getting pagination result for events. Please check database connection.");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Guid> CreateAsync(CreateEventDTO eventDTO)
    {
        try
        {
            var eventData = _mapper.Map<Event>(eventDTO);

            eventData.CreatedById = eventDTO.UserId;
            eventData.Created = DateTime.UtcNow;

            await _dbContext.Event.AddAsync(eventData);

            await _dbContext.SaveChangesAsync();

            return eventData.Id;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<int> UpdateAsync(Guid id, UpdateEventDTO eventDTO, Guid userId)
    {
        try
        {
            var result = await _dbContext.Event
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                throw new Exception($"Could not find event with id={id}. Possible missing id or event is soft deleted.");
            }

            if (result.UserId != userId)
            {
                throw new AuthenticationException($"User with id={userId} is not authorized to update event with id={id}.");
            }

            result.Name = eventDTO.Name;
            result.Description = eventDTO.Description;
            result.EventAccomodationType = eventDTO.EventAccomodationTypes;
            result.EventDetailsLink = eventDTO.EventDetailsLink;
            result.EventStartsAt = eventDTO.EventStartsAt;
            result.EventEndsAt = eventDTO.EventEndsAt;
            result.EventType = eventDTO.EventType;
            result.IsMoreInfoAvailable = eventDTO.IsMoreInfoAvailable;
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

            throw new KeyNotFoundException($"Could not delete event with id={id}. Possible missing id or request is not from creator.");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
