
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ValetaxTestTree.Api.BackgroundWorkers;
using ValetaxTestTree.Api.Factories;
using ValetaxTestTree.Api.Middleware;
using ValetaxTestTree.Application;
using ValetaxTestTree.Infrastructure;

namespace ValetaxTestTree.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHostedService<MessageBusListener>();
            builder.Services.AddControllers()
                .AddJsonOptions(
                    options =>
                    {
                        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.WriteAsString;
                    }
                );

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddPersistence(connectionString);

            var busHostName = builder.Configuration.GetSection("MessageBus").GetValue<string>("Host");
            builder.Services.AddMessaging(busHostName);

            builder.Services.RegisterRequestHandlers();

            builder.Services.AddTransient<ErrorHandlerMiddleware>();
            builder.Services.AddSingleton<ICreateJournalEventCommandFactory, CreateJournalEventCommandFactory>();
            builder.Services.AddSingleton<IErrorResultFactory, ErrorResultFactory>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);
            });

            var app = builder.Build();
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next(context);
            });
            app.UseMiddleware<ErrorHandlerMiddleware>();

            await app.Services.InitializeDatabaseAsync();
            if (app.Environment.IsDevelopment())
                await app.Services.InitializeDatabaseTestDataAsync();

            await app.Services.InitializeMessagingAsync();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
