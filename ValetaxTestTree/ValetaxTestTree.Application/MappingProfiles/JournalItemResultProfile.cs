using AutoMapper;
using ValetaxTestTree.Application.Models;
using ValetaxTestTree.Domain.Entities;

namespace ValetaxTestTree.Application.MappingProfiles
{
    public class JournalItemResultProfile : Profile
    {
        public JournalItemResultProfile()
        {
            CreateMap<JournalEvent, JournalItemResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Timestamp));
        }
    }
}
