﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.MeetingEvent
{
    public record MeetingStartDateUpdated(Meeting meeting, DateTime startDate) : IDomainEvent;
}
