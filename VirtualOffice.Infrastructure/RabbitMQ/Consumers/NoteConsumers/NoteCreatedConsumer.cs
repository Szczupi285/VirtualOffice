using MassTransit;
using VirtualOffice.Application.IntegrationEvents.NoteCreatedIntegrationEvent;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.NoteConsumers
{
    public class NoteCreatedConsumer : IConsumer<NoteCreatedIntegrationEvent>
    {
        private readonly NotesService _notesService;

        public NoteCreatedConsumer(NotesService notesService)
        {
            _notesService = notesService;
        }

        public async Task Consume(ConsumeContext<NoteCreatedIntegrationEvent> context)
        {
            await _notesService.CreateAsync(context.Message);
        }
    }
}