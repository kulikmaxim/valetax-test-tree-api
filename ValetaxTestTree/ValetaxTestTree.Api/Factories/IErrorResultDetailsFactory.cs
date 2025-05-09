using ValetaxTestTree.Api.Models;

namespace ValetaxTestTree.Api.Factories
{
    public interface IErrorResultDetailsFactory
    {
        public ErrorResult Create(Exception exception, long? eventId);
    }
}
