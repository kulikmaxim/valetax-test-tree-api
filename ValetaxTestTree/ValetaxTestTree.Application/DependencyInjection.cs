using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ValetaxTestTree.Application.Handlers;
using ValetaxTestTree.Application.MappingProfiles;
using ValetaxTestTree.Application.Validators;

namespace ValetaxTestTree.Application
{
    public static class DependencyInjection
    {
        public static void RegisterRequestHandlers(this IServiceCollection services) 
        {
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetOrCreateTreeCommandHandler>());
            services.AddAutoMapper(typeof(TreeResultProfile).Assembly);
            services.AddValidatorsFromAssemblyContaining<GetOrCreateTreeCommandValidator>();
        }
    }
}
