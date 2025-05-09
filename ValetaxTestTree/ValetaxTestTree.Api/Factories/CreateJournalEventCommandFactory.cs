using System.Text;
using ValetaxTestTree.Api.Extensions;
using ValetaxTestTree.Application.Requests;

namespace ValetaxTestTree.Api.Factories
{
    public class CreateJournalEventCommandFactory : ICreateJournalEventCommandFactory
    {
        public async Task<CreateJournalEventCommand> CreateAsync(HttpContext context, Exception exception)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            ArgumentNullException.ThrowIfNull(exception, nameof(exception));

            var timestamp = DateTime.UtcNow;
            var eventId = timestamp.Ticks;
            var info = await GetInfo(eventId, context, exception);
            var result = new CreateJournalEventCommand
            {
                EventId = timestamp.Ticks,
                Timestamp = timestamp,
                Info = info
            };

            return result;
        }

        private static async Task<string> GetInfo(
            long requestId,
            HttpContext context,
            Exception exception) 
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.AppendLine($"Request ID = {requestId}");
            infoBuilder.AppendLine($"Method = {context.Request.Method}");
            infoBuilder.AppendLine($"Query = {context.Request.Path}{context.Request.QueryString}");

            var body = await context.Request.GetBodyStringAsync();
            if (!string.IsNullOrWhiteSpace(body))
                infoBuilder.AppendLine($"Body params:{Environment.NewLine}{body}");

            infoBuilder.AppendLine(exception.ToString());

            return infoBuilder.ToString();
        }
    }
}
