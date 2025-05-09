using ValetaxTestTree.Api.Models;
using ValetaxTestTree.Application.Exceptions;

namespace ValetaxTestTree.Api.Factories
{
    public class ErrorResultFactory : IErrorResultDetailsFactory
    {
        public ErrorResult Create(Exception exception, long? eventId)
        {
            ArgumentNullException.ThrowIfNull(exception, nameof(exception));
            if (eventId.HasValue && eventId.Value <= 0)
                throw new ArgumentOutOfRangeException(nameof(eventId));

            var (type, message) = GetData(eventId, exception);
            var errorResult = new ErrorResult
            {
                Type = type,
                Id = eventId,
                Data = new()
                {
                    Message = message,
                }
            };

            return errorResult;
        }

        private (string type, string message) GetData(long? eventId, Exception exception)
        {
            const string exceptionName = nameof(Exception);
            if (exception is SecureException)
            {
                var type = exception.GetType().Name
                    .Replace(exceptionName, string.Empty);

                return (type, exception.Message);
            }

            var message = "Internal server error";
            if (eventId.HasValue)
                message += $" ID = {eventId.Value}";

            return (exceptionName, message);
        }
    }
}
