using ApiConsumer.RabbitHandlers;
using App.RabbitMQ.Bus.BusRabbit;
using App.RabbitMQ.Bus.EventsQueue;
using App.RabbitMQ.Bus.Implement;
using MediatR;
using System.Reflection;

namespace ApiConsumer.Infrastructure.Startup
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
            builder.Services.AddTransient<IRabbitEventBus, RabbitEventBus>();
            builder.Services.AddTransient<IEventHandler<EmailEventQueue>,EmailEventHandler>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(x => x.WithEvaluator(y => y.Namespace != null && y.Namespace.StartsWith("ApiConsumer")),
                                                                    typeof(EmailEventHandler).GetTypeInfo().Assembly);

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
