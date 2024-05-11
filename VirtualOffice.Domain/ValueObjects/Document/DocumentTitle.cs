using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Document;
using VirtualOffice.Domain.Exceptions.Office;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.ValueObjects.Document
{
    public sealed record DocumentTitle : AbstractRecordName
    {
        public DocumentTitle(string value) : base(value, 50, new EmptyDocumentTitleException(), new TooLongDocumentTitleException(value))
        {
        }

        public static implicit operator DocumentTitle(string title)
            => new(title);
    }
}
