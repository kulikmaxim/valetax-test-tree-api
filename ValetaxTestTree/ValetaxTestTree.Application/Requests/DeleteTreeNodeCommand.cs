using MediatR;

namespace ValetaxTestTree.Application.Requests
{
    public class DeleteTreeNodeCommand : IRequest
    {
        public string TreeName { get; set; }
        public int NodeId { get; set; }
    }
}
