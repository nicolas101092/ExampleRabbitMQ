using ApiConsumer.RabbitHandlers;
using App.RabbitMQ.Bus.BusRabbit;
using App.RabbitMQ.Bus.EventsQueue;

namespace ApiConsumer.Infrastructure.Startup
{
    /// <summary>
    /// Class for configure the pipeline of the api execution
    /// </summary>
    public static class WebApplicationExtension
    {
        /// <summary>
        /// method for configure the services of the api
        /// </summary>
        /// <param name="app">a object WebApplication to configure the pipeline</param>
        public static void Configure(this WebApplication app)
        {
            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var eventBus = ((IApplicationBuilder) app).ApplicationServices.GetRequiredService<IRabbitEventBus>();
            eventBus.Subscribe<EmailEventQueue, EmailEventHandler>();

            app.Run();
        }
    }
}
