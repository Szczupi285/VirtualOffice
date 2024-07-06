using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record RemoveAssignedEmployeesToEmployeeTask(Guid guid, HashSet<ApplicationUser> employeesToRemove) : ICommand;

}
