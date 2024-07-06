﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.DomainEvents.NoteEvent
{
    public record NoteTitleChanged(Note note, NoteTitle title) : IDomainEvent;
}
