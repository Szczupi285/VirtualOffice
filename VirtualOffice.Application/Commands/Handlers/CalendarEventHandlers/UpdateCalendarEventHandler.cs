using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    public class UpdateCalendarEventHandler : IRequestHandler<UpdateCalendarEvent>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public UpdateCalendarEventHandler(ICalendarEventRepository repository, ICalendarEventReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdateCalendarEvent request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
                {
                    var (Id, Title, EventDescription, StartDate, EndDate) = request;

                    if (!await _readService.ExistsByIdAsync(Id))
                        throw new CalendarEventDoesNotExistException(Id);

                    var calEv = await _repository.GetByIdAsync(Id);

                    // we update only changed properties rather than whole object
                    // beacuse for example changing the title to the same title would raise an event.
                    if (calEv._Title != Title)
                        calEv.SetTitle(Title);
                    if (calEv._Description != EventDescription)
                        calEv.SetDescription(EventDescription);
                    if (calEv._StartDate != StartDate)
                        calEv.UpdateStartDate(StartDate);
                    if (calEv._EndDate != EndDate)
                        calEv.UpdateEndDate(EndDate);

                    await _repository.UpdateAsync(calEv);

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