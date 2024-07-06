using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers
{
   

    public class RemoveAssignedEmployeesFromEmployeeTaskHandler : ICommandHandler<RemoveAssignedEmployeesToEmployeeTask>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;

        public RemoveAssignedEmployeesFromEmployeeTaskHandler(ICalendarEventRepository repository, ICalendarEventReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task HandleAsync(RemoveAssignedEmployeesToEmployeeTask command, CancellationToken cancellationToken)
        {

            if (!await _readService.ExistsByIdAsync(command.guid))
            {
                throw new CalendarEventDoesNotExistException(command.guid);
            }

            var calEv = await _repository.GetById(command.guid);
            calEv.RemoveEmployeesRange(command.employeesToRemove);
            await _repository.Update(calEv);
        }
    }
}
