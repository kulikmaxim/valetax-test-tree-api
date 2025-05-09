namespace ValetaxTestTree.Domain.Entities
{
    public class JournalEvent : IEntity
    {
        public int Id { get; set; }
        public long EventId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Info { get; set; }
    }
}
