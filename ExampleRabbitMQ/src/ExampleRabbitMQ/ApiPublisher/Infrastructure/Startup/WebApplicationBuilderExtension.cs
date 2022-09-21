using App.RabbitMQ.Bus.BusRabbit;
using App.RabbitMQ.Bus.Implement;
using Application.Services.ApiPublisher.Commands.Test;
using Domain.Common.Configuration;
using MediatR;
using System.Reflection;

namespace ApiPublisher.Infrastructure.Startup
{
    /// <summary>
    /// Class for configure the aplication
    /// </summary>
    public static class WebApplicationBuilderExtension
    {
        /// <summary>
        /// method for configure the services of api-rest
        /// </summary>
        /// <param name="builder">a object WebApplicationBuilder with the configuration</param>
        /// <returns>a object WebApplicationBuilder with the result</returns>
        public static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScrutor(nameof(ApiPublisher));
            builder.Services.AddTransient<IRabbitEventBus, RabbitEventBus>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(x => x.WithEvaluator(y => y.Namespace != null && y.Namespace.StartsWith("ApiPublisher")),
                                                                    typeof(CreateTestCommandHandler).GetTypeInfo().Assembly);

            return builder;
        }

        /// <summary>
        /// method for use the services of api-rest
        /// </summary>
        /// <param name="builder">a object WebApplicationBuilder with the configuration</param>
        public static void Configure(this WebApplicationBuilder builder)
        {
            builder.Build().Configure();
        }
    }
}
