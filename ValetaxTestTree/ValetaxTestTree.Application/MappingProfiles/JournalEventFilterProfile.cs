using AutoMapper;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Domain.Models;

namespace ValetaxTestTree.Application.MappingProfiles
{
    public class JournalEventFilterProfile : Profile
    {
        public JournalEventFilterProfile()
        {
            CreateMap<GetJournalEventRangeQuery, BaseFilter<JournalEventCriteria>>()
                .ForMember(dest => dest.Skip, opt => opt.MapFrom(src => src.Skip))
                .ForMember(dest => dest.Take, opt => opt.MapFrom(src => src.Take))
                .ForPath(dest => dest.Criteria.From, opt => opt.MapFrom(src => src.From))
                .ForPath(dest => dest.Criteria.To, opt => opt.MapFrom(src => src.To))
                .ForPath(dest => dest.Criteria.Search, opt => opt.MapFrom(src => src.Search));
        }
    }
}
