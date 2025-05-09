using AutoMapper;
using ValetaxTestTree.Application.Models;
using ValetaxTestTree.Domain.Models;


namespace ValetaxTestTree.Application.MappingProfiles
{
    public class RangeResultProfile : Profile
    {
        public RangeResultProfile()
        {
            CreateMap(typeof(PageResult<>), typeof(RangeResult<>));
        }
    }
}
