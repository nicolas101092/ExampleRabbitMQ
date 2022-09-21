using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Domain.Common.Configuration
{
    /// <summary>
    /// Class for inject the services in the api
    /// </summary>
    public static class ConfigureServiceExtensioncs
    {
        /// <summary>
        /// method for automate dependency injection
        /// </summary>
        /// <param name="services">a object IServiceCollection to inject the dependences</param>
        /// <param name="apiName">the name of appi for filter of api-rest</param>
        /// <returns>a object IServiceCollection with the result</returns>
        public static IServiceCollection AddScrutor(this IServiceCollection services, string apiName)
        {
            services.Scan(s => s.FromAssemblies(Assembly.Load("Infrastructure"), Assembly.Load("Application"))
                    .AddClasses(c => c.Where(e =>
                    {
                        bool nameSpace = e.Namespace != null && e.Namespace.StartsWith($"Application.Services.{apiName}");

                        return nameSpace;
                    }))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
