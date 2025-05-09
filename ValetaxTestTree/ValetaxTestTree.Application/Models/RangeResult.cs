using System.Collections.Generic;

namespace ValetaxTestTree.Application.Models
{
    public class RangeResult<T>
    {
        public int Skip { get; set; }
        public int Count { get; set; }
        public ICollection<T> Items { get; set; }
    }
}
