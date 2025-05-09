using MediatR;
using ValetaxTestTree.Api.Factories;
using ValetaxTestTree.Application.Requests;

namespace ValetaxTestTree.Api.Middleware
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly IMediator mediator;
        private readonly ICreateJournalEventCommandFactory createJournalEventCommandFactory;
        private readonly ILogger<ErrorHandlerMiddleware> logger;
        private readonly IErrorResultDetailsFactory problemDetailsFactory;

        public ErrorHandlerMiddleware(
            IMediator mediator,
            ICreateJournalEventCommandFactory createJournalEventCommandFactory,
            ILogger<ErrorHandlerMiddleware> logger,
            IErrorResultDetailsFactory problemDetailsFactory)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.createJournalEventCommandFactory = createJournalEventCommandFactory
                ?? throw new ArgumentNullException(nameof(createJournalEventCommandFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.problemDetailsFactory = problemDetailsFactory
                ?? throw new ArgumentNullException(nameof(problemDetailsFactory));
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
                createCommand = await createJournalEventCommandFactory
                    .CreateAsync(context, exception);
                await mediator.Send(createCommand);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occurred during saving JournalEvent");
            }

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var errorResult = problemDetailsFactory.Create(
                exception,
                createCommand?.EventId);
            await context.Response.WriteAsJsonAsync(errorResult);
        }
    }
}
