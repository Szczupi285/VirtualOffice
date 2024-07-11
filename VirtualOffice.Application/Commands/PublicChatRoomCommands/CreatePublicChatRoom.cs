using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ChatRoom;

namespace VirtualOffice.Application.Commands.PublicChatRoomCommands
{
    public record CreatePublicChatRoom(HashSet<ApplicationUser> Participants, SortedSet<Message> Messages, string Name) : IRequest;
}
