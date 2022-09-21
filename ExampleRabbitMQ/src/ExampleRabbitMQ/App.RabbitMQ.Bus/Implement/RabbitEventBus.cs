using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace App.RabbitMQ.Bus.Implement
{
    public class RabbitEventBus : IRabbitEventBus
    {
        private readonly IMediator _mediator = null!;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;

        public RabbitEventBus(IMediator mediator) 
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Publish<T>(T evento) where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var eventName = evento.GetType().Name;

            channel.QueueDeclare(eventName, false, false, false, null);

            var message = JsonConvert.SerializeObject(evento);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("", eventName, null, body);
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerEventType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
                _eventTypes.Add(typeof(T));

            if (!_handlers.ContainsKey(eventName))
                _handlers.Add(eventName, new List<Type>());

            if (_handlers[eventName].Any(x => x.GetType() == handlerEventType))
                throw new ArgumentException($"El manejador {handlerEventType.Name} fue registrado anteriormente por {eventName}");

            _handlers[eventName].Add(handlerEventType);

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Delegate;

            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task Consumer_Delegate(object sender, BasicDeliverEventArgs e)
        {
            var nombreEvento = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                if (_handlers.ContainsKey(nombreEvento)) {

                    var subscriptions = _handlers[nombreEvento];
                    foreach (var sb in subscriptions) {
                        var manejador = Activator.CreateInstance(sb);
                        if (manejador == null) continue;

                        var tipoEvento = _eventTypes.SingleOrDefault(x => x.Name == nombreEvento);
                        var eventoDS = JsonConvert.DeserializeObject(message, tipoEvento);

                        var concretoTipo = typeof(IEventHandler<>).MakeGenericType(tipoEvento);

                        await (Task)concretoTipo.GetMethod("Handle").Invoke(manejador, new object[] { eventoDS });
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
}
