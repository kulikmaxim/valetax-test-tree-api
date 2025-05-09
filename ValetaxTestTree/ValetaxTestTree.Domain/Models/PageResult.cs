namespace ValetaxTestTree.Domain.Models
{
    public class PageResult<T>
    {
        public int Skip { get; set; }
        public int Count { get; set; }
        public ICollection<T> Items { get; set; }
    }
}
