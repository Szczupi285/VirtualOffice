using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Exceptions.EmployeeTask;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers
{
    public class AddAssignedEmployeesToEmployeeTaskHandler : IRequestHandler<AddAssignedEmployeesToEmployeeTask>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;

        public AddAssignedEmployeesToEmployeeTaskHandler(ICalendarEventRepository repository, ICalendarEventReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(AddAssignedEmployeesToEmployeeTask request, CancellationToken cancellationToken)
        {

            if (!await _readService.ExistsByIdAsync(request.Guid))
            {
                throw new EmployeeTaskDoesNotExistsException(request.Guid);
            }

            var calEv = await _repository.GetById(request.Guid);
            calEv.AddEmployeesRange(request.EmployeesToAdd);
            await _repository.Update(calEv);
        }
    }
}
