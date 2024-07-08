using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VirtualOffice.Domain.Entities;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Commands;
using MediatR;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record AddCalendarEventAssignedEmployees(Guid Guid, HashSet<ApplicationUser> EmployeesToAdd) : IRequest;

}
