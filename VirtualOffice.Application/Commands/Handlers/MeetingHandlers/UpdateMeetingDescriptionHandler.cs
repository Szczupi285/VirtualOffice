using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Application.Exceptions.Meeting;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.MeetingHandlers
{
    internal sealed class UpdateMeetingDescriptionHandler : IRequestHandler<UpdateMeetingDescription>
    {
        private readonly IMeetingRepository _repository;
        private readonly IMeetingReadService _readService;
        private readonly IMediator _mediator;
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public UpdateMeetingDescriptionHandler(IMeetingRepository repository, IMeetingReadService eventReadService, IMediator mediator)
        {
            _repository = repository;
            _readService = eventReadService;
            _mediator = mediator;
        }

        public async Task Handle(UpdateMeetingDescription request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
                {
                    if (!await _readService.ExistsByIdAsync(request.Id, cancellationToken))
                        throw new MeetingDoesNotExistException(request.Id);

                    var meeting = await _repository.GetByIdAsync(request.Id, cancellationToken);

                    if (meeting._Description != request.Description)
                        meeting.SetDescription(request.Description);
                    await _repository.UpdateAsync(meeting, cancellationToken);

                    foreach (var domainEvent in meeting.Events)
                        await _mediator.Publish(domainEvent, cancellationToken);
                    meeting.ClearEvents();

                    break;
                }
                catch (DbUpdateConcurrencyException)
                {
                    _retryCount++;

                    // rethrowing exception after max attempts
                    if (_retryCount >= _maxRetryAttempts)
                        throw;

                    // each retry takes place 2x later than previous one e.g. 200ms => 400ms => 800ms
                    await Task.Delay(TimeSpan.FromMilliseconds(Math.Pow(2, _retryCount) * 100), cancellationToken);
                }
            }
        }
    }
}