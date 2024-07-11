using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Application.Commands.CreatePrivateDocumentCommands
{
    public record CreatePrivateDocument(DocumentId Id, DocumentContent Content, DocumentTitle Title, DocumentCreationDate CreationDate, DocumentFilePath FilePath) : IRequest;
}
