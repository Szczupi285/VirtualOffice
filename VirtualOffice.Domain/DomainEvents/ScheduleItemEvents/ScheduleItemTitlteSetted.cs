﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.DomainEvents.ScheduleItemEvents
{
    public record ScheduleItemTitleSetted(AbstractScheduleItem abstractScheduleItem, ScheduleItemTitle title) : IDomainEvent;
}
