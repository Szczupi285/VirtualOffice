using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.MeetingCommands
{
    public record CreateMeeting(Guid guid, string title, string description, HashSet<ApplicationUser> assignedEmployees, DateTime startDate, DateTime endDate) : ICommand;
    
}
