using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.DomainEvents;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract class AggregateRoot<T>
    {
        public T Id { get; protected set; }
        public int Version { get; protected set; }
        public IEnumerable<IDomainEvent> Events => _Events;

        private readonly List<IDomainEvent> _Events = new();

        private bool _versionIncremented;


        protected void AddEvent(IDomainEvent @event)
        {
            if(!_Events.Any() && !_versionIncremented)
            {
                Version++;
                _versionIncremented = true;

            }
            _Events.Add(@event);
        }
        public void ClearEvents() => _Events.Clear();
        protected void IncrementVersion()
        {
            if (_versionIncremented)
            {
                return;
            }
            Version++;
            _versionIncremented = true;
        }
    }
}
