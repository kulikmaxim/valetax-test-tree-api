using AutoMapper;
using ValetaxTestTree.Application.Models;
using ValetaxTestTree.Domain.Entities;

namespace ValetaxTestTree.Application.MappingProfiles
{
    public class JournalEventResultProfile : Profile
    {
        public JournalEventResultProfile()
        {
            CreateMap<JournalEvent, JournalEventResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Info));
        }
    }
}
