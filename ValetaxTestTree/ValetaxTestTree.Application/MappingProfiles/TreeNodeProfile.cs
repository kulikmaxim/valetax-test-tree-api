using AutoMapper;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Domain.Entities;

namespace ValetaxTestTree.Application.MappingProfiles
{
    public class TreeNodeProfile : Profile
    {
        public TreeNodeProfile()
        {
            CreateMap<CreateTreeNodeCommand, TreeNode>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NodeName))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParendNodeId));

            CreateMap<GetOrCreateTreeCommand, TreeNode>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TreeName));
        }
    }
}
