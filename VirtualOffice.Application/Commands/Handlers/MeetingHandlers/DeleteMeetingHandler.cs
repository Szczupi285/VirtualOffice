using MediatR;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Application.Exceptions.Meeting;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.MeetingHandlers
{
    internal sealed class DeleteMeetingHandler : IRequestHandler<DeleteMeeting>
    {
        private readonly IMeetingRepository _repository;
        private readonly IMeetingReadService _readService;
        private readonly IMediator _mediator;

        public DeleteMeetingHandler(IMeetingRepository repository, IMeetingReadService eventReadService, IMediator mediator)
        {
            _repository = repository;
            _readService = eventReadService;
            _mediator = mediator;
        }

        public async Task Handle(DeleteMeeting request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new MeetingDoesNotExistException(request.Id);

            var entity = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(entity);
            entity.Disable();

            foreach (var domainEvent in entity.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            entity.ClearEvents();
        }
    }
}