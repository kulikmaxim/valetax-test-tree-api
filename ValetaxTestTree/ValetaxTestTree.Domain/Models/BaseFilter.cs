namespace ValetaxTestTree.Domain.Models
{
    public class BaseFilter<T>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public T Criteria { get; set; }
    }
}
