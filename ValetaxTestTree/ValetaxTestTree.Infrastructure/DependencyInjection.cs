using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using ValetaxTestTree.Domain.Repositories;
using ValetaxTestTree.Infrastructure.Extensions;
using ValetaxTestTree.Infrastructure.Messaging;
using ValetaxTestTree.Infrastructure.Messaging.Consumers;
using ValetaxTestTree.Infrastructure.Messaging.Models;
using ValetaxTestTree.Infrastructure.Messaging.Publishers;
using ValetaxTestTree.Infrastructure.Repositories;
using ValetaxTestTree.Infrastructure.Storage;

namespace ValetaxTestTree.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(
                opt => opt.UseNpgsql(connectionString),
                ServiceLifetime.Transient,
                ServiceLifetime.Transient);

            services.AddTransient(typeof(IReadOnlyRepository<>), typeof(BaseReadOnlyRepository<>));
            services.AddTransient(typeof(IReadWriteRepository<>), typeof(BaseReadWriteRepository<>));
            services.AddTransient<ITreeNodeRepository, TreeNodeRepository>();
            services.AddTransient<IJournalEventRepository, JournalEventRepository>();
        }

        public static void AddMessaging(this IServiceCollection services, string busHostName)
        {
            services.AddSingleton<IMessageBusConnectionProvider>(new RabbitMqConnectionProvider(busHostName));
            services.AddScoped<IJournalEventMessagePublisher, JournalEventMessagePublisher>();
            services.AddSingleton<IJournalEventMessageConsumer, JournalEventMessageConsumer>();
        }

        public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await appDbContext.Database.MigrateAsync();
        }

        public static async Task InitializeDatabaseTestDataAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await AppDbInitializer.SeedData(appDbContext);
        }

        public static async Task InitializeMessagingAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var busConnectionProvider = scope.ServiceProvider.GetRequiredService<IMessageBusConnectionProvider>();
            var channel = busConnectionProvider.Channel;

            await channel.ExchangeDeclareAsync(
                exchange: Exchange.JournalEvent.GetDescription(), 
                type: ExchangeType.Direct);
            await channel.QueueDeclareAsync(
                queue: Queue.AddJournalEvent.GetDescription(),
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            await channel.QueueBindAsync(
                Queue.AddJournalEvent.GetDescription(),
                Exchange.JournalEvent.GetDescription(),
                Queue.AddJournalEvent.GetDescription());
        }
    }
}
