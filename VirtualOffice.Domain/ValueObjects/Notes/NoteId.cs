using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.Notes
{
    public sealed record NoteId : AbstractRecordId
    {
        public NoteId(Guid value) : base(value, new EmptyApplicationUserIdException())
        {
        }

        public static implicit operator NoteId(Guid id)
            => new(id);
    }
}
