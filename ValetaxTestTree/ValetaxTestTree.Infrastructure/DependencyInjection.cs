using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ValetaxTestTree.Domain.Repositories;
using ValetaxTestTree.Infrastructure.Repositories;

namespace ValetaxTestTree.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));

            services.AddScoped(typeof(IReadOnlyRepository<>), typeof(BaseReadOnlyRepository<>));
            services.AddScoped(typeof(IReadWriteRepository<>), typeof(BaseReadWriteRepository<>));
            services.AddScoped<ITreeNodeRepository, TreeNodeRepository>();
            services.AddScoped<IJournalEventRepository, JournalEventRepository>();
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
    }
}
