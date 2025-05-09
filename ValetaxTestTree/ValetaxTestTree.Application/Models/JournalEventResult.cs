using System;

namespace ValetaxTestTree.Application.Models
{
    public class JournalEventResult
    {
        public int Id { get; set; }
        public long EventId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
