using MediatR;
using ValetaxTestTree.Application.Models;

namespace ValetaxTestTree.Application.Requests
{
    public class GetOrCreateTreeCommand : IRequest<TreeResult>
    {
        public string TreeName { get; set; }
    }
}
