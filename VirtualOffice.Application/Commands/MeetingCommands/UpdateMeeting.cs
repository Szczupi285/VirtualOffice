using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.MeetingCommands
{
    public record UpdateMeeting(Guid guid, string title, string description, DateTime startDate, DateTime endDate) : ICommand;

}
