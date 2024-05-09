using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Exceptions.EmployeeTask;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;

namespace VirtualOffice.Domain.Entities
{
    public class EmployeeTask
    {
        public EmployeeTaskId Id { get; private set; }
        public EmployeeTaskTitle _Title {get; private set;}
        public EmployeeTaskDescription _Description {get; private set;}
        public ICollection<ApplicationUser> _AssignedEmployees { get; private set; }
        public EmployeeTaskPriorityEnum _Priority {get; private set;}
        public EmployeeTaskStatusEnum _TaskStatus{ get; private set; } = EmployeeTaskStatusEnum.NotStarted;

        // start date is not set to default Utc.Now because it will be avalible to create tasks that are supposed to be started in the future
        public EmployeeTaskStartDate _StartDate {get; private set;}
        public EmployeeTaskEndDate _EndDate {get; private set;}


        public EmployeeTask(EmployeeTaskId id, EmployeeTaskTitle title, EmployeeTaskDescription description,
            ICollection<ApplicationUser> assignedEmployees, EmployeeTaskPriorityEnum priority,
           EmployeeTaskStartDate startDate, EmployeeTaskEndDate endDate)
        {
            if (startDate.Value >= endDate.Value)
                throw new EmployeeTaskEndDateCannotBeBeforeStartDate(endDate, startDate);

            Id = id;
            _Title = title;
            _Description = description;
            _AssignedEmployees = assignedEmployees;
            _Priority = priority;
            _StartDate = startDate;
            _EndDate = endDate;
        }

        public void SetTitle(string title) => _Title = title;
        public void SetDescription(string description) => _Description = _Description;
        public void SetPriority(EmployeeTaskPriorityEnum priority) => _Priority = priority;
        public void SetStatus(EmployeeTaskStatusEnum Status) => _TaskStatus = Status;

        public void AddEmployee(ApplicationUser user) 
        {
            if (_AssignedEmployees.Contains(user))
                throw new UserIsAlreadyAssignedToThisTaskException(user.Id);

            _AssignedEmployees.Add(user);
        }
        public void AddEmployeesRange(ICollection<ApplicationUser> users)
        {
            foreach (var user in users)
                AddEmployee(user);
        }

        public void RemoveEmployee(ApplicationUser user)
        {
            if (!_AssignedEmployees.Contains(user))
                throw new UserIsNotAssignedToThisTaskException(user.Id);

               _AssignedEmployees.Remove(user);
        }

        public void RemoveEmployeesRange(ICollection<ApplicationUser> users)
        {
            foreach(var user in users)
                RemoveEmployee(user);
        }

        public void UpdateEndDate(DateTime endDate)
        {
            if(endDate <= _StartDate)
                throw new EmployeeTaskEndDateCannotBeBeforeStartDate(endDate, _StartDate);

            _EndDate = endDate;
        }



    }
}
