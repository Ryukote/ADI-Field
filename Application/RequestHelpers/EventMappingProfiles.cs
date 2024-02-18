using Application.DTOs.Event;
using AutoMapper;
using Domain.Entities;

namespace Application.RequestHelpers
{
    public class EventMappingProfiles : Profile
    {
        public EventMappingProfiles()
        {
            CreateMap<CreateEventDTO, Event>();
            CreateMap<Event, EventDTO>();
        }
    }
}
