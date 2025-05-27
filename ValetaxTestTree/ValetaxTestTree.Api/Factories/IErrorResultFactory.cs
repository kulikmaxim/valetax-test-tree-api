using ValetaxTestTree.Api.Models;

namespace ValetaxTestTree.Api.Factories
{
    public interface IErrorResultFactory
    {
        public ErrorResult Create(Exception exception, long? eventId);
    }
}
