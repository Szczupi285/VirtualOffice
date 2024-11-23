using MassTransit;
using VirtualOffice.Application.IntegrationEvents.NoteIntegrationEvent;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.NoteConsumers
{
    public class NoteTitleUpdatedConsumer : IConsumer<NoteTitleChangedIntegrationEvent>
    {
        private readonly NotesService _notesService;

        public NoteTitleUpdatedConsumer(NotesService notesService)
        {
            _notesService = notesService;
        }

        public async Task Consume(ConsumeContext<NoteTitleChangedIntegrationEvent> context)
        {
            await _notesService.UpdateTitleAsync(context.Message.Id, context.Message.Title);
        }
    }
}