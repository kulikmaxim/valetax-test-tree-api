using MediatR;

namespace ValetaxTestTree.Application.Requests
{
    public class RenameTreeNodeCommand : IRequest
    {
        public string TreeName { get; set; }
        public int NodeId { get; set; }
        public string NewNodeName { get; set; }
    }
}
