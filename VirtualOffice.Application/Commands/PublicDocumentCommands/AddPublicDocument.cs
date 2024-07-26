using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Application.Commands.PublicDocumentCommands
{
    public record AddPublicDocument(string content, string title, Guid userId,
        ICollection<ApplicationUserId> eligibleForRead, ICollection<ApplicationUserId> eligibleForWrite,
        ICollection<DocumentFilePath> attachmentFilePaths) : IRequest
    {
    }
}
