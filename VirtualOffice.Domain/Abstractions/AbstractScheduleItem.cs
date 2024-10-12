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
            AddEvent(new ScheduleItemTitleSetted(this, title, GetType()));
        }

        public void SetDescription(string description)
        {
            _Description = description;
            AddEvent(new ScheduleItemDescriptionSetted(this, description, GetType()));
        }

        public bool AddEmployee(ApplicationUser user)
        {
            bool HasBeenAdded = _AssignedEmployees.Add(user);

            if (HasBeenAdded)
                AddEvent(new EmployeeAddedToScheduleItem(this, user));

            return HasBeenAdded;
        }

        public void AddEmployeesRange(ICollection<ApplicationUser> users)
        {
            var addedEmployees = new HashSet<ApplicationUser>();
            foreach (var user in users)
            {
                bool hasBeenAdded = AddEmployee(user);
                if (hasBeenAdded)
                    addedEmployees.Add(user);
            }
            if (addedEmployees.Any())
                // we use bulk event to handle database synchronization so we can update the read db just once.
                // more granular operations like SendEmail will be resolved in EmployeeAddedToScheduleItem EventHandler
                AddEvent(new BulkEmployeesAddedToScheduleItem(this, addedEmployees, GetType()));
        }

        public bool RemoveEmployee(ApplicationUser user)
        {
            if (!_AssignedEmployees.Contains(user))
                throw new UserIsNotAssignedToThisScheduleItemException(user.Id);

            bool IsRemoved = _AssignedEmployees.Remove(user);
            if (IsRemoved)
                AddEvent(new EmployeeRemovedFromScheduleItem(this, user));

            return IsRemoved;
        }

        public void RemoveEmployeesRange(ICollection<ApplicationUser> users)
        {
            var removedEmployees = new HashSet<ApplicationUser>();

            foreach (var user in users)
            {
                bool isRemoved = RemoveEmployee(user);
                if (isRemoved)
                    removedEmployees.Add(user);
            }

            if (removedEmployees.Any())
                // we use bulk event to handle database synchronization so we can update the read db just once.
                // more granular operations like SendEmail will be resolved in EmployeeAddedToScheduleItem EventHandler
                AddEvent(new BulkEmployeesRemovedFromScheduleItem(this, removedEmployees, GetType()));
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