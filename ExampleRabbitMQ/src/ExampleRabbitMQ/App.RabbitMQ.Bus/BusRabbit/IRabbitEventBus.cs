namespace App.RabbitMQ.Bus.BusRabbit
{
    public interface IRabbitEventBus
    {
        Task SendCommand<T>(T command) where T : Command;

        void Publish<T>(T evento) where T : Event;

        void Subscribe<T, TH>() where T : Event
                                where TH : IEventHandler<T>;
    }
}