using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Event;
using VirtualOffice.Domain.Exceptions.Organization;
using VirtualOffice.Domain.ValueObjects.Event;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract class AbstractEvent
    {
        public EventId Id { get;  }
        public EventTitle _Title { get; private set; }
        public EventDescription _Description { get; private set; }
        public EventStartDate _StartDate { get; private set; }
        public EventEndDate _EndDate { get; private set; }
        public ICollection<ApplicationUser> _VisibleTo { get; private set; }

        protected AbstractEvent(EventId id, EventTitle titile, EventStartDate startDate, EventEndDate endDate,
            EventDescription eventDescription, ICollection<ApplicationUser> visibleTo)
        {
            Id = id;
            _Title = titile;
            _StartDate = startDate;
            _EndDate = endDate;
            _Description = eventDescription;
            _VisibleTo = visibleTo;
        }

        public void EditTitle(string title) => _Title = title;

        public void EditDescription(string description) => _Description = description;

        public void EditStartDate(DateTime startDate) => _StartDate = startDate;

        public void EditEndDate(DateTime endDate) => _EndDate = endDate;

        public void AddUserToVisibleTo(ApplicationUser user)
        {
            bool aleadyExists = _VisibleTo.Any(u => u.Id == user.Id);

            if (aleadyExists)
                throw new ThisEventIsAlreadyVIsibleToThisUser(user.Id);
            _VisibleTo.Add(user);
        }
        public void AddUsersRangeToVisibleTo(ICollection<ApplicationUser> users)
        {
            foreach(ApplicationUser user in users)
            {
                AddUserToVisibleTo(user);
            }
        }

        public void RemoveUserFromVisibleTo(ApplicationUser user)
        {
            bool alreadyExists = _VisibleTo.Any(u => u.Id == user.Id);

            if (!alreadyExists)
                throw new UserIsNotFoundInVisibleToCollection(user.Id);
            _VisibleTo.Remove(user);
        }

        public void RemoveUsersRangeFromVisibleTo(ICollection<ApplicationUser> users)
        {
            foreach (ApplicationUser user in users)
            {
                RemoveUserFromVisibleTo(user);
            }
        }
    }
}
