using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Application.Exceptions.Meeting;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.MeetingHandlers
{
    public class RescheduleMeetingHandler : IRequestHandler<RescheduleMeeting>
    {
        private readonly IMeetingRepository _repository;
        private readonly IMeetingReadService _readService;
        private readonly IMediator _mediator;
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public RescheduleMeetingHandler(IMeetingRepository repository, IMeetingReadService readService, IMediator mediator)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
        }

        public async Task Handle(RescheduleMeeting request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
                {
                    if (!await _readService.ExistsByIdAsync(request.Id, cancellationToken))
                        throw new MeetingDoesNotExistException(request.Id);

                    var meeting = await _repository.GetByIdAsync(request.Id, cancellationToken);

                    meeting.RescheduleScheduleItem(request.StartDate, request.EndDate);

                    await _repository.UpdateAsync(meeting, cancellationToken);

                    foreach (var domainEvent in meeting.Events)
                        await _mediator.Publish(domainEvent, cancellationToken);
                    meeting.ClearEvents();

                    break;
                }
                catch (DbUpdateConcurrencyException)
                {
                    _retryCount++;

                    if (_retryCount >= _maxRetryAttempts)
                        throw;
                    // each retry takes place 2x later than previous one e.g. 200ms => 400ms => 800ms
                    await Task.Delay(TimeSpan.FromMilliseconds(Math.Pow(2, _retryCount) * 100), cancellationToken);
                }
            }
        }
    }
}