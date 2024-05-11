using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.Exceptions.Document;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.Document
{
    public sealed record DocumentId : AbstractRecordId
    {
        public DocumentId(Guid value) : base(value, new EmptyDocumentIdException())
        {
        }

        public static implicit operator DocumentId(Guid id)
            => new(id);

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
