using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record CreateEmployeeTask(Guid Guid, string Title, string Description,
        HashSet<ApplicationUser> AssignedEmployees, DateTime StartDate, DateTime EndDate,
        EmployeeTaskPriorityEnum Priority) : IRequest;

}
