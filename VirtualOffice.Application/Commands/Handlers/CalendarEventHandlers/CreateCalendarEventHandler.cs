using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.Handlers
{
    public class CreateCalendarEventHandler : ICommandHandler<CreateCalendarEvent>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;

        public CreateCalendarEventHandler(ICalendarEventRepository repository, ICalendarEventReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task HandleAsync(CreateCalendarEvent command, CancellationToken cancellationToken)
        {

            var (id, title, eventDescription, assignedEmployees, startDate, endDate) = command;

            if (await _readService.ExistsByIdAsync(id))
            {
                throw new CalendarEventAlreadyExistsException(id);
            }
            CalendarEvent calEv = new CalendarEvent(id, title, eventDescription, assignedEmployees, startDate, endDate);

            await _repository.Add(calEv);
        }
    }
}
