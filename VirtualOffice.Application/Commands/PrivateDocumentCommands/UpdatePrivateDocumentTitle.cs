using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Application.Commands.PrivateDocumentCommands
{
    public record UpdatePrivateDocumentTitle(Guid Id, string Title) : IRequest;
}
