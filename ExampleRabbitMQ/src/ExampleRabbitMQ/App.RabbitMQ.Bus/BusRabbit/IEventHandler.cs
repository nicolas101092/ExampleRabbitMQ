namespace App.RabbitMQ.Bus.BusRabbit
{
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent: Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler { }
}