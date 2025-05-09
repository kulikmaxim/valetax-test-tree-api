using MediatR;

namespace ValetaxTestTree.Application.Requests
{
    public class CreateTreeNodeCommand : IRequest
    {
        public string TreeName { get; set; }
        public int ParendNodeId { get; set; }
        public string NodeName { get; set; }
    }
}
