using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    public class UpdateCalendarEventTitleHandler : IRequestHandler<UpdateCalendarEventTitle>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;
        private readonly IMediator _mediator;
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public UpdateCalendarEventTitleHandler(ICalendarEventRepository repository, ICalendarEventReadService readService, IMediator mediator)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
        }

        public async Task Handle(UpdateCalendarEventTitle request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
                {
                    if (!await _readService.ExistsByIdAsync(request.Id))
                        throw new CalendarEventDoesNotExistException(request.Id);

                    var calEv = await _repository.GetByIdAsync(request.Id);

                    // we update only changed properties rather than whole object
                    // beacuse for example changing the title to the same title would raise an event.
                    if (calEv._Title != request.Title)
                        calEv.SetTitle(request.Title);
                    //if (calEv._Description != EventDescription)
                    //    calEv.SetDescription(EventDescription);
                    //if (calEv._StartDate != StartDate)
                    //    calEv.UpdateStartDate(StartDate);
                    //if (calEv._EndDate != EndDate)
                    //    calEv.UpdateEndDate(EndDate);

                    await _repository.UpdateAsync(calEv);

                    foreach (var domainEvent in calEv.Events)
                        await _mediator.Publish(domainEvent, cancellationToken);
                    calEv.ClearEvents();

                    break;
                }
                catch (DbUpdateConcurrencyException)
                {
                    _retryCount++;

                    // rethrowing exception after max attempts
                    if (_retryCount >= _maxRetryAttempts)
                        throw;

                    // wait for certain ammount of time between entries;
                    await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
                }
            }
        }
    }
}