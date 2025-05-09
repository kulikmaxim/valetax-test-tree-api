using AutoMapper;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Domain.Entities;


namespace ValetaxTestTree.Application.MappingProfiles
{
    public class JournalEventProfile : Profile
    {
        public JournalEventProfile()
        {
            CreateMap<CreateJournalEventCommand, JournalEvent>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.Info, opt => opt.MapFrom(src => src.Info));
        }
    }
}
