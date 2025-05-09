using AutoMapper;
using ValetaxTestTree.Application.Models;
using ValetaxTestTree.Domain.Entities;

namespace ValetaxTestTree.Application.MappingProfiles
{
    public class TreeResultProfile : Profile
    {
        public TreeResultProfile()
        {
            CreateMap<TreeNode, TreeResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));
        }
    }
}
