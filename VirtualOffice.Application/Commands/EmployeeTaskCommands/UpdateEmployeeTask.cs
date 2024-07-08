using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record UpdateEmployeeTask(Guid Id, string Title, string Description, 
        DateTime EndDate, EmployeeTaskStatusEnum Status, EmployeeTaskPriorityEnum Priority) : IRequest;

}
