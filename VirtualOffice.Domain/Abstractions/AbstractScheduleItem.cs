﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract class AbstractScheduleItem
    {

        public ScheduleItemId Id { get; private set; }
        public ScheduleItemTitle _Title { get; private set; }
        public ScheduleItemDescription _Description { get; private set; }
        public HashSet<ApplicationUser> _AssignedEmployees { get; private set; }
        // start date is not set to default Utc.Now because it will be avalible to create tasks that are supposed to be started in the future
        public ScheduleItemStartDate _StartDate { get; private set; }
        public ScheduleItemEndDate _EndDate { get; private set; }

        public AbstractScheduleItem(ScheduleItemId id, ScheduleItemTitle title, ScheduleItemDescription description,
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

        public void SetTitle(string title) => _Title = title;
        public void SetDescription(string description) => _Description = description;

        public void AddEmployee(ApplicationUser user)
        {
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
                throw new UserIsNotAssignedToThisScheduleItemException(user.Id);

            _AssignedEmployees.Remove(user);
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
        }
    }
}
