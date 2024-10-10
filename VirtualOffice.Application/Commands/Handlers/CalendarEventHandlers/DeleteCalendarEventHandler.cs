using MediatR;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    public class DeleteCalendarEventHandler : IRequestHandler<DeleteCalendarEvent>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;
        private readonly IMediator _mediator;

        public DeleteCalendarEventHandler(ICalendarEventRepository repository, ICalendarEventReadService readService, IMediator mediator)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
        }

        public async Task Handle(DeleteCalendarEvent request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new CalendarEventDoesNotExistException(request.Id);

            var entity = await _repository.GetByIdAsync(request.Id);
            entity.Disable();

            foreach (var domainEvent in entity.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            entity.ClearEvents();

            await _repository.DeleteAsync(entity);
        }
    }
}