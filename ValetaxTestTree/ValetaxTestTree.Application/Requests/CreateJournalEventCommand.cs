using System;
using MediatR;

namespace ValetaxTestTree.Application.Requests
{
    public class CreateJournalEventCommand : IRequest
    {
        public long EventId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Info { get; set; }
    }
}
