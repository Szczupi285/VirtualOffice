using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers
{
    public class UpdateEmployeeTaskDescriptionHandler : IRequestHandler<UpdateEmployeeTaskDescription>
    {
        private readonly IEmployeeTaskRepository _repository;
        private readonly IEmployeeTaskReadService _readService;
        private readonly IMediator _mediator;
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public UpdateEmployeeTaskDescriptionHandler(IEmployeeTaskRepository repository, IEmployeeTaskReadService readService, IMediator mediator)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
        }

        public async Task Handle(UpdateEmployeeTaskDescription request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
                {
                    if (!await _readService.ExistsByIdAsync(request.Id, cancellationToken))
                        throw new CalendarEventDoesNotExistException(request.Id);

                    var empTask = await _repository.GetByIdAsync(request.Id, cancellationToken);

                    if (empTask._Description != request.Description)
                        empTask.SetDescription(request.Description);

                    await _repository.UpdateAsync(empTask, cancellationToken);

                    foreach (var domainEvent in empTask.Events)
                        await _mediator.Publish(domainEvent, cancellationToken);
                    empTask.ClearEvents();

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