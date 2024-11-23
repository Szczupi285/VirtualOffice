using MassTransit;
using VirtualOffice.Application.IntegrationEvents.NoteIntegrationEvent;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.NoteConsumers
{
    public class NoteDisabledConsumer : IConsumer<NoteDisabledIntegrationEvent>
    {
        private readonly NotesService _notesService;

        public NoteDisabledConsumer(NotesService notesService)
        {
            _notesService = notesService;
        }

        public async Task Consume(ConsumeContext<NoteDisabledIntegrationEvent> context)
        {
            await _notesService.RemoveAsync(context.Message.Id);
        }
    }
}