using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Application.Commands.PrivateDocumentCommands
{
    public record UpdatePrivateDocument(ChatRoomId Id, DocumentContent Content, DocumentTitle Title, DocumentCreationDate CreationDate, DocumentFilePath FilePath)
    {
    }
}
