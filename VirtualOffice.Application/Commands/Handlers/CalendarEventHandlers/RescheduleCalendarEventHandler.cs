using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    public class RescheduleCalendarEventHandler : IRequestHandler<RescheduleCalendarEvent>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;
        private readonly IMediator _mediator;
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public RescheduleCalendarEventHandler(ICalendarEventRepository repository, ICalendarEventReadService readService, IMediator mediator)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
        }

        public async Task Handle(RescheduleCalendarEvent request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
                {
                    if (!await _readService.ExistsByIdAsync(request.Id))
                        throw new CalendarEventDoesNotExistException(request.Id);

                    var calEv = await _repository.GetByIdAsync(request.Id, cancellationToken);

                    calEv.RescheduleCalendarEvent(request.StartDate, request.EndDate);

                    await _repository.UpdateAsync(calEv, cancellationToken);

                    foreach (var domainEvent in calEv.Events)
                        await _mediator.Publish(domainEvent, cancellationToken);
                    calEv.ClearEvents();

                    break;
                }
                catch (DbUpdateConcurrencyException)
                {
                    _retryCount++;

                    if (_retryCount >= _maxRetryAttempts)
                        throw;

                    await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
                }
            }

            // throw exception if failed
        }
    }
}