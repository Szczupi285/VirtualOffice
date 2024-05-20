using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.NoteEvent
{
    public record NoteTitleChanged(Note note) : IDomainEvent;
}
