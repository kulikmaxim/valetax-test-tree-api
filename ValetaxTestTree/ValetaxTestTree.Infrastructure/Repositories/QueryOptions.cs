namespace ValetaxTestTree.Infrastructure.Repositories
{
    public class QueryOptions
    {
        public QueryOptions(bool asNoTracking = false, bool asSplitQuery = false)
        {
            AsNoTracking = asNoTracking;
            AsSplitQuery = asSplitQuery;
        }

        public bool AsNoTracking { get; set; }

        public bool AsSplitQuery { get; set; }
    }
}
