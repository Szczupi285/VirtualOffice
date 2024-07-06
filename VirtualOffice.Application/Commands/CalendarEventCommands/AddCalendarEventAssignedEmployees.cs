using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VirtualOffice.Domain.Entities;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record AddCalendarEventAssignedEmployees(Guid guid, HashSet<ApplicationUser> employeesToAdd) : ICommand;

}
