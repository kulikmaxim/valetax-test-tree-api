using MediatR;
using ValetaxTestTree.Application.Models;

namespace ValetaxTestTree.Application.Requests
{
    public class GetJournalEventQuery : IRequest<JournalEventResult>
    {
        public int Id { get; set; }
    }
}
