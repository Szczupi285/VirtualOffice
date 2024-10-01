using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers;

public class UpdateCalendarEventTitleHandler : IRequestHandler<UpdateCalendarEventTitle>
{
    private readonly ICalendarEventRepository _repository;
    private readonly ICalendarEventReadService _readService;
    private const int _maxRetryAttempts = 3;
    private int _retryCount = 0;

    public UpdateCalendarEventTitleHandler(ICalendarEventRepository repository, ICalendarEventReadService readService)
    {
        _repository = repository;
        _readService = readService;
    }

    public async Task Handle(UpdateCalendarEventTitle request, CancellationToken cancellationToken)
    {
        while (_retryCount < _maxRetryAttempts)
        {
            try
            {
                var (Id, Title) = request;

                if (!await _readService.ExistsByIdAsync(Id))
                    throw new CalendarEventDoesNotExistException(Id);

                var calEv = await _repository.GetByIdAsync(Id);

                // we update only changed properties
                // beacuse for example changing the title to the same title would raise an event.
                if (calEv._Title != Title)
                    calEv.SetTitle(Title);

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