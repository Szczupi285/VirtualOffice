﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.DomainEvents.AbstractDocumentEvents
{
    public record DocumentPreviousVersionSetted(AbstractDocument document, AbstractDocument previousVersion) : IDomainEvent;
}
