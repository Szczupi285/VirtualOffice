using MassTransit;
using VirtualOffice.Application.IntegrationEvents.NoteIntegrationEvent;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.NoteConsumers
{
    public class NoteContentUpdatedConsumer : IConsumer<NoteContentChangedIntegrationEvent>
    {
        private readonly NotesService _notesService;

        public NoteContentUpdatedConsumer(NotesService notesService)
        {
            _notesService = notesService;
        }

        public async Task Consume(ConsumeContext<NoteContentChangedIntegrationEvent> context)
        {
            await _notesService.UpdateContentAsync(context.Message.Id, context.Message.Content);
        }
    }
}