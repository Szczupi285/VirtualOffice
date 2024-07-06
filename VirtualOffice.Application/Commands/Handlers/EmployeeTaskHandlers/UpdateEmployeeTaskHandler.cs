using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Exceptions.EmployeeTask;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers
{
    public class UpdateEmployeeTaskHandler : ICommandHandler<UpdateEmployeeTask>
    {
        private readonly IEmployeeTaskRepository _repository;
        private readonly IEmployeeTaskReadService _readService;

        public UpdateEmployeeTaskHandler(IEmployeeTaskRepository repository, IEmployeeTaskReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task HandleAsync(UpdateEmployeeTask command, CancellationToken cancellationToken)
        {

            var (id, title, description, endDate, status, priority) = command;

            if (!await _readService.ExistsByIdAsync(id))
            {
                throw new EmployeeTaskDoesNotExistsException(id);
            }

            var empTask = await _repository.GetById(id);

            // we update only changed properties rather than whole object 
            // beacuse changing the title to the same title would raise an event.
            if (empTask._Title != title)
                empTask.SetTitle(title);
            if (empTask._Description != description)
                empTask.SetDescription(description);
            if (empTask._EndDate != endDate)
                empTask.UpdateEndDate(endDate);
            if (empTask._TaskStatus != status)
                empTask.UpdateStatus(status);
            if (empTask._Priority != priority)
                empTask.SetPriority(priority);

            await _repository.Update(empTask);
        }
    }
}
