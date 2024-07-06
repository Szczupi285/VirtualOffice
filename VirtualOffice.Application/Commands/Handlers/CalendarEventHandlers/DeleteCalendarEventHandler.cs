using System;
using System.Collections.Generic;
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
    public class DeleteCalendarEventHandler : ICommandHandler<DeleteCalendarEvent>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;

        public DeleteCalendarEventHandler(ICalendarEventRepository repository, ICalendarEventReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task HandleAsync(DeleteCalendarEvent command, CancellationToken cancellationToken)
        {

            if (!await _readService.ExistsByIdAsync(command.guid))
            {
                throw new CalendarEventDoesNotExistException(command.guid);
            }

            var calEv = _repository.Delete(command.guid);
        }
    }
}
