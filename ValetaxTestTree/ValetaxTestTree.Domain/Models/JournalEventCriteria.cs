namespace ValetaxTestTree.Domain.Models
{
    public class JournalEventCriteria
    {
        public DateTimeOffset? From { get; set; }
        public DateTimeOffset? To { get; set; }
        public string Search { get; set; }
    }
}
