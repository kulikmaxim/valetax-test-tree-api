using System.Collections.Generic;

namespace ValetaxTestTree.Application.Models
{
    public class TreeResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TreeResult> Children { get; set; }
    }
}
