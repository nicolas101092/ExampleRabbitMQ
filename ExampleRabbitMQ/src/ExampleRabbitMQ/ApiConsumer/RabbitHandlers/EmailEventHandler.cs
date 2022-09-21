using App.RabbitMQ.Bus.BusRabbit;
using App.RabbitMQ.Bus.EventsQueue;

namespace ApiConsumer.RabbitHandlers
{
    public class EmailEventHandler : IEventHandler<EmailEventQueue>
    {
        public EmailEventHandler() { }
                
        public Task Handle(EmailEventQueue @event)
        {
            return Task.CompletedTask;
        }
    }
}
