using VirtualOffice.Domain.DomainEvents.ScheduleItem;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract class AbstractScheduleItem : AggregateRoot<ScheduleItemId>
    {
        public ScheduleItemTitle _Title { get; private set; }
        public ScheduleItemDescription _Description { get; private set; }
        public HashSet<ApplicationUser> _AssignedEmployees { get; private set; }

        // start date is not set to default Utc.Now because it will be avalible to create tasks that are supposed to be started in the future
        public ScheduleItemStartDate _StartDate { get; private protected set; }

        public ScheduleItemEndDate _EndDate { get; private protected set; }

        protected AbstractScheduleItem(ScheduleItemId id, ScheduleItemTitle title, ScheduleItemDescription description,
          HashSet<ApplicationUser> assignedEmployees, ScheduleItemStartDate startDate, ScheduleItemEndDate endDate)
        {
            // Review: commented for testing purposes this causes an error while updating CalendarEvent
            if (startDate.Value >= endDate.Value)
                throw new EndDateCannotBeBeforeStartDate(endDate, startDate);

            Id = id;
            _Title = title;
            _Description = description;
            _AssignedEmployees = assignedEmployees;
            _StartDate = startDate;
            _EndDate = endDate;
        }

        protected AbstractScheduleItem()
        { }

        public void SetTitle(string title)
        {
            _Title = title;
            AddEvent(new ScheduleItemTitleSetted(this, title));
        }

        public void SetDescription(string description)
        {
            _Description = description;
            AddEvent(new ScheduleItemDescriptionSetted(this, description));
        }

        public void AddEmployee(ApplicationUser user)
        {
            bool HasBeenAdded = _AssignedEmployees.Add(user);

            if (HasBeenAdded)
                AddEvent(new EmployeeAddedToScheduleItem(this, user));
        }

        public void AddEmployeesRange(ICollection<ApplicationUser> users)
        {
            foreach (var user in users)
                AddEmployee(user);
        }

        public void RemoveEmployee(ApplicationUser user)
        {
            if (!_AssignedEmployees.Contains(user))
                throw new UserIsNotAssignedToThisScheduleItemException(user.Id);

            _AssignedEmployees.Remove(user);
            AddEvent(new EmployeeRemovedFromScheduleItem(this, user));
        }

        public void RemoveEmployeesRange(ICollection<ApplicationUser> users)
        {
            foreach (var user in users)
                RemoveEmployee(user);
        }

        public void UpdateEndDate(DateTime endDate)
        {
            if (endDate <= _StartDate)
                throw new EndDateCannotBeBeforeStartDate(endDate, _StartDate);

            _EndDate = endDate;
            AddEvent(new ScheduleItemEndDateUpdated(this, endDate));
        }
    }
}