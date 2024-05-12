using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Event;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract class AbstractEvent
    {
        public EventId Id { get; private set; }
        public EventTitle Title { get; private set; }
        public EventStartDate StartDate { get; private set; }
        public EventEndDate EndDate { get; private set; }
        public EventDescription Description { get; private set; }
        public ICollection<ApplicationUser> VisibleTo { get; private set; }
    }
}
