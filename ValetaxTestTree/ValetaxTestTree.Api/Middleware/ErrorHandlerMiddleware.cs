using ValetaxTestTree.Api.Factories;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Infrastructure.Messaging.Publishers;

namespace ValetaxTestTree.Api.Middleware
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly IJournalEventMessagePublisher journalEventMessagePublisher;
        private readonly ICreateJournalEventCommandFactory createJournalEventCommandFactory;
        private readonly ILogger<ErrorHandlerMiddleware> logger;
        private readonly IErrorResultFactory errorResultFactory;

        public ErrorHandlerMiddleware(
            IJournalEventMessagePublisher journalEventMessagePublisher,
            ICreateJournalEventCommandFactory createJournalEventCommandFactory,
            ILogger<ErrorHandlerMiddleware> logger,
            IErrorResultFactory errorResultFactory)
        {
            this.journalEventMessagePublisher = journalEventMessagePublisher
                ?? throw new ArgumentNullException(nameof(journalEventMessagePublisher));
            this.createJournalEventCommandFactory = createJournalEventCommandFactory
                ?? throw new ArgumentNullException(nameof(createJournalEventCommandFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.errorResultFactory = errorResultFactory
                ?? throw new ArgumentNullException(nameof(errorResultFactory));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);

                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            CreateJournalEventCommand createCommand = null;
            try
            {
                createCommand = await createJournalEventCommandFactory.CreateAsync(context, exception);
                await journalEventMessagePublisher.PublishJournalEventAsync(createCommand);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occurred during publishing JournalEvent");
            }

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var errorResult = errorResultFactory.Create(
                exception,
                createCommand?.EventId);
            await context.Response.WriteAsJsonAsync(errorResult);
        }
    }
}
