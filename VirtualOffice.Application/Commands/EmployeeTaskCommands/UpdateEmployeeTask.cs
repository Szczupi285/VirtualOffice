using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record UpdateEmployeeTask(Guid id, string title, string description, 
        DateTime endDate, EmployeeTaskStatusEnum status, EmployeeTaskPriorityEnum priority) : ICommand;

}
