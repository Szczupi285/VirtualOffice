﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.MeetingEvent
{
    public record MeetingRescheduled(Meeting Meeting) : IDomainEvent;
}
