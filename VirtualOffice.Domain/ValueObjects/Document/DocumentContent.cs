using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Document;
using VirtualOffice.Domain.ValueObjects.Organization;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.ValueObjects.Document
{
    public sealed record DocumentContent : AbstractRecordName
    {
        public DocumentContent(string value) : base(value, 100000, new EmptyDocumentContentException())
        {
        }

        public static implicit operator DocumentContent(string content)
            => new(content);
    }
}
