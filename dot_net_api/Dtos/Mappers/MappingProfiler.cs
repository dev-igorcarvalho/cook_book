using AutoMapper;
using dot_net_api.Models;

namespace dot_net_api.Dtos.Mappers
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
        }
    }
}