using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.PublicChatRoomCommands
{
    public record AddChatParticipants(Guid Id, HashSet<ApplicationUser> Participants) : IRequest;
}
