using App.RabbitMQ.Bus.BusRabbit;
using App.RabbitMQ.Bus.EventsQueue;
using Application.Services.ApiPublisher.DtoModels.Test.Requests;
using MediatR;

namespace Application.Services.ApiPublisher.Commands.Test
{
    public class CreateTestCommandHandler : IRequestHandler<DtoCreateTestRequest, bool>
    {
        private readonly IRabbitEventBus _rabbitEventBus;

        public CreateTestCommandHandler(IRabbitEventBus rabbitEventBus)
        {
            _rabbitEventBus = rabbitEventBus;
        }

        public async Task<bool> Handle(DtoCreateTestRequest request, CancellationToken cancellationToken)
        {
            _rabbitEventBus.Publish(new EmailEventQueue("Contenido nuevo", "Mi nuevo destinatario", "El titulo"));

            return true;
        }
    }
}
