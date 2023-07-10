using AutoMapper;
using Domain.Dto;
using Domain.Models;

namespace Automapper.Application;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<FlightApiDto, Flight>()
        .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src.DepartureStation))
        .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.ArrivalStation))
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
        .ForMember(dest => dest.Transport, opt => opt.MapFrom(src => new Transport()
        {
            FlightCarrier = src.FlightCarrier,
            FlightNumber = src.FlightNumber
        }));
    }
}