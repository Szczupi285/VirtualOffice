﻿using MediatR;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    public class RemoveCalendarEventAssignedEmployeesHandler : IRequestHandler<RemoveCalendarEventAssignedEmployees>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;
        private readonly IMediator _mediator;

        public RemoveCalendarEventAssignedEmployeesHandler(ICalendarEventRepository repository, ICalendarEventReadService readService, IMediator mediator)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
        }

        public async Task Handle(RemoveCalendarEventAssignedEmployees request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new CalendarEventDoesNotExistException(request.Id);

            var calEv = await _repository.GetByIdAsync(request.Id);
            calEv.RemoveEmployeesRange(request.EmployeesToRemove);
            await _repository.UpdateAsync(calEv);

            foreach (var domainEvent in calEv.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            calEv.ClearEvents();
        }
    }
}