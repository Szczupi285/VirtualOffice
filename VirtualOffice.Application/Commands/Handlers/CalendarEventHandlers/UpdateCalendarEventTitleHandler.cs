using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    internal sealed class UpdateCalendarEventTitleHandler : IRequestHandler<UpdateCalendarEventTitle>
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
                    if (!await _readService.ExistsByIdAsync(request.Id, cancellationToken))
                        throw new CalendarEventDoesNotExistException(request.Id);

                    var calEv = await _repository.GetByIdAsync(request.Id, cancellationToken);

                    if (calEv._Title != request.Title)
                        calEv.SetTitle(request.Title);

                    await _repository.UpdateAsync(calEv, cancellationToken);

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

                    // each retry takes place 2x later than previous one e.g. 200ms => 400ms => 800ms
                    await Task.Delay(TimeSpan.FromMilliseconds(Math.Pow(2, _retryCount) * 100), cancellationToken);
                }
            }
        }
    }
}