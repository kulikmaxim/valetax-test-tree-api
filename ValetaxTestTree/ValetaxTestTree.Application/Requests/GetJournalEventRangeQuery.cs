using System;
using MediatR;
using ValetaxTestTree.Application.Models;

namespace ValetaxTestTree.Application.Requests
{
    public class GetJournalEventRangeQuery : IRequest<RangeResult<JournalItemResult>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public DateTimeOffset? From { get; set; }
        public DateTimeOffset? To { get; set; }
        public string Search { get; set; }
    }
}
