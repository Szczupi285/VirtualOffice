﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace DomainUnitTests
{
    public class EmployeeTaskUnitTests
    {
        private EmployeeTask _EmployeeTask { get; set; }
        private ApplicationUser _ApplicationUser { get; set; }
        private ApplicationUser _ApplicationUser2 { get; set; }
        private ApplicationUser _ApplicationUser3 { get; set; }

        public EmployeeTaskUnitTests()
        {

            ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "NameOne" , "SurnameOne");
            ApplicationUser user2 = new ApplicationUser(Guid.NewGuid(), "NameTwo" , "SurnameTwo");
            ApplicationUser user3 = new ApplicationUser(Guid.NewGuid(), "NameThree" , "SurnameThree");
            _ApplicationUser = user1;
            _ApplicationUser2 = user2;
            _ApplicationUser3 = user3;

            HashSet<ApplicationUser> users = new HashSet<ApplicationUser>();

            users.Add(user1);
            users.Add(user2);
            users.Add(user3);
            Guid guid = Guid.NewGuid();

            EmployeeTask employeeTask = new EmployeeTask(guid, "title", "description",
                users, EmployeeTaskPriorityEnum.Low, DateTime.UtcNow, DateTime.UtcNow.AddHours(8));

            _EmployeeTask = employeeTask;
        }


        #region ScheduleItemId

        [Fact]
        public void EmptyScheduleItemId_ShouldReturnEmptyScheduleItemIdException()
        {
            Assert.Throws<EmptyEmployeeScheduleItemIdException>(()
                => new ScheduleItemId(Guid.Empty));
        }
        [Fact]
        public void ValidScheduleItemId_ValidGuidToAppUserIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            ScheduleItemId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidScheduleItemId_ValidAppUserIdToGuidConversionShouldEqual()
        {

            ScheduleItemId id = new ScheduleItemId(Guid.NewGuid());

            Guid guid = id;

            Assert.Equal(id.Value, guid);
        }
        #endregion

        #region ScheduleItemTitle

        [Fact]
        public void EmptyScheduleItemTitle_ShouldReturnEmptyScheduleItemTitleException()
        {
            Assert.Throws<EmptyScheduleItemTitleException>(()
                => new ScheduleItemTitle(""));
        }

        [Fact]
        public void NullScheduleItemTitle_ShouldReturnEmptyScheduleItemTitleException()
        {
            Assert.Throws<EmptyScheduleItemTitleException>(()
                => new ScheduleItemTitle(null));
        }

        [Fact]
        public void ValidScheduleItemTitle_ValidStringToScheduleItemTitleConversion_ShouldEqual()
        {
            string title = "ExampleTitle";

            ScheduleItemTitle tit = title;

            Assert.Equal(title, tit);
        }
        [Fact]
        public void ValidScheduleItemTitle_ValidScheduleItemTitleToStringConversionShouldEqual()
        {
            ScheduleItemTitle tit = new ScheduleItemTitle("ExampleTitle");

            string title = tit;

            Assert.Equal(tit, title);

        }
        [Fact]
        public void InvalidScheduleItemTitle_TooLongTitle()
        {
            string s = new string('a', 101);
            Assert.Throws<TooLongScheduleItemTitleException>(() => new ScheduleItemTitle(s));
        }
        [Fact]
        public void ValidScheduleItemTitle_HundredChars()
        {
            string s = new string('a', 100);
            new ScheduleItemTitle(s);
        }
        [Fact]
        public void ValidScheduleItemTitle_OneChar()
        {
            new ScheduleItemTitle("a");
        }

        #endregion

        #region ScheduleItemDescription 
        [Fact]
        public void ValidScheduleItemDescription_ValidStringToScheduleItemDescriptionConversion_ShouldEqual()
        {
            string Description = "ExampleDescription";

            ScheduleItemDescription desc = Description;

            Assert.Equal(Description, desc);
        }
        [Fact]
        public void ValidScheduleItemDescription_ValidScheduleItemDescriptionToStringConversionShouldEqual()
        {
            ScheduleItemDescription desc = new ScheduleItemDescription("ExampleDescription");

            string Description = desc;

            Assert.Equal(desc, Description);

        }
        [Fact]
        public void InvalidScheduleItemDescription_TooLongDescription()
        {
            string s = new string('a', 1501);
            Assert.Throws<TooLongScheduleItemDescriptionException>(() => new ScheduleItemDescription(s));
        }
        [Fact]
        public void ValidScheduleItemDescription_HundredChars()
        {
            string s = new string('a', 1500);
            new ScheduleItemDescription(s);
        }
        [Fact]
        public void ValidScheduleItemDescription_OneChar()
        {
            new ScheduleItemDescription("a");
        }
        [Fact]
        public void ValidScheduleItemDescription_WhiteSpace()
        {
            new ScheduleItemDescription(" ");
        }
        [Fact]
        public void ValidScheduleItemDescription_Empty()
        {
            new ScheduleItemDescription("");
        }
        #endregion

        #region ScheduleItemStartDate

        [Fact]
        public void ScheduleItemStartDate31daysBeforeNow_ScheduleItemStartDateCannotBePastException()
        {
            Assert.Throws<ScheduleItemStartDateCannotBePastException>(()
                => new ScheduleItemStartDate
                (
                    DateTime.UtcNow.AddDays(-31))
                );
        }
        [Fact]
        public void ScheduleItemStartDateValid31daysFromNow_ShouldNotThrowException()
        {
            ScheduleItemStartDate startDate = DateTime.UtcNow.AddDays(31);
        }
        [Fact]
        public void ScheduleItemStartDateValidYearFromNow_ShouldNotThrowException()
        {
            ScheduleItemStartDate startDate = DateTime.UtcNow.AddYears(1);
        }
        [Fact]
        public void ScheduleItemStartDateValidCurrentTime_ShouldNotThrowException()
        {
            ScheduleItemStartDate startDate = DateTime.UtcNow;
        }

        [Fact]
        public void ValidData_DateTimeToScheduleItemStartDateConversion_ShouldEqual()
        {
            var dt = DateTime.UtcNow.AddDays(31);

            ScheduleItemStartDate startDate = dt;

            Assert.Equal(dt, startDate);
        }
        [Fact]
        public void ValidData_ScheduleItemStartDateToDateTimeConversionShouldEqual()
        {
            ScheduleItemStartDate startDate = new ScheduleItemStartDate(DateTime.UtcNow.AddDays(31));


            var dt = startDate;


            Assert.Equal(startDate, dt);
        }


        #endregion

        #region ScheduleItemEndDate

        [Fact]
        public void ScheduleItemEndDate1dayBeforeNow_ScheduleItemEndDateCannotBePastException()
        {
            Assert.Throws<InvalidScheduleItemEndDateException>(()
                => new ScheduleItemEndDate
                (
                    DateTime.UtcNow.AddDays(-1))
                );
        }
        [Fact]
        public void ScheduleItemEndDateInvalidCurrentTime_ShouldThrowInvalidScheduleItemEndDateException()
        {
            Assert.Throws<InvalidScheduleItemEndDateException>(()
                => new ScheduleItemEndDate
                (
                    DateTime.UtcNow
                ));
        }
        [Fact]
        public void ScheduleItemEndDateValid31daysFromNow_ShouldNotThrowException()
        {
            ScheduleItemEndDate EndDate = DateTime.UtcNow.AddDays(31);
        }
        [Fact]
        public void ScheduleItemEndDateValidYearFromNow_ShouldNotThrowException()
        {
            ScheduleItemEndDate EndDate = DateTime.UtcNow.AddYears(1);
        }


        [Fact]
        public void ValidData_DateTimeToScheduleItemEndDateConversion_ShouldEqual()
        {
            var dt = DateTime.UtcNow.AddDays(31);

            ScheduleItemEndDate EndDate = dt;

            Assert.Equal(dt, EndDate);
        }
        [Fact]
        public void ValidData_ScheduleItemEndDateToDateTimeConversionShouldEqual()
        {
            ScheduleItemEndDate EndDate = new ScheduleItemEndDate(DateTime.UtcNow.AddDays(31));


            var dt = EndDate;


            Assert.Equal(EndDate, dt);
        }

        #endregion

        #region Properties
        [Fact]
        public void ScheduleItemStatus_DefaultStatus_ShouldBeNotStarted()
        {
            Assert.Equal(EmployeeTaskStatusEnum.NotStarted, _EmployeeTask._TaskStatus);
        }
        #endregion

        #region methods
        [Fact]
        public void EmployeeTask_SetTitle_ProperlySetted()
        {
            string title = "ChangedTitle";
            _EmployeeTask.SetTitle(title);
            Assert.Equal(title, _EmployeeTask._Title);
        }

        [Fact]
        public void EmployeeTask_SetDescription_ProperlySetted()
        {
            string description = "ChangedDescription";
            _EmployeeTask.SetDescription(description);
            Assert.Equal(description, _EmployeeTask._Description);
        }
        [Fact]
        public void EmployeeTask_SetPriority_ProperlySetted()
        {
            _EmployeeTask.SetPriority(EmployeeTaskPriorityEnum.Urgent);
            Assert.Equal(EmployeeTaskPriorityEnum.Urgent, _EmployeeTask._Priority);
        }
        [Fact]
        public void EmployeeTask_UpdateStatus_ProperlySetted()
        {
            _EmployeeTask.UpdateStatus(EmployeeTaskStatusEnum.InProgress);
            Assert.Equal(EmployeeTaskStatusEnum.InProgress, _EmployeeTask._TaskStatus);
        }
        [Fact]
        public void AddEmployee_UserNotAssignedPreviously_ListShouldContainUser()
        {
            ApplicationUser user4 = new ApplicationUser(Guid.NewGuid(), "NameFour", "SurnameFour");
            _EmployeeTask.AddEmployee(user4);
            Assert.Contains(user4, _EmployeeTask._AssignedEmployees);
        }
        [Fact]
        public void AddEmployeesRange_UserNotAssignedPreviously_ListShouldContainUsers()
        {
            ApplicationUser user4 = new ApplicationUser(Guid.NewGuid(), "NameFour", "SurnameFour");
            ApplicationUser user5 = new ApplicationUser(Guid.NewGuid(), "NameFive", "SurnameFive");
            ApplicationUser user6 = new ApplicationUser(Guid.NewGuid(), "NameSix", "SurnameSix");
            List<ApplicationUser> usersList = new List<ApplicationUser>() {user4, user5, user6 };
            _EmployeeTask.AddEmployeesRange(usersList);
            Assert.True(_EmployeeTask._AssignedEmployees.Contains(user4) &&
                _EmployeeTask._AssignedEmployees.Contains(user5) &&
                _EmployeeTask._AssignedEmployees.Contains(user6));
        }

        [Fact]
        public void RemoveEmployee_EmployeeIsAlreadyAssigned_ShouldRemoveEmployee()
        {
            Assert.Contains(_ApplicationUser,_EmployeeTask._AssignedEmployees);
            _EmployeeTask.RemoveEmployee(_ApplicationUser);
            Assert.DoesNotContain(_ApplicationUser,_EmployeeTask._AssignedEmployees);
        }
        [Fact]
        public void RemoveEmployee_EmployeeNotAssigned_ShouldThrowUserIsNotAssignedToThisScheduleItemException()
        {
            ApplicationUser user4 = new ApplicationUser(Guid.NewGuid(), "NameFour", "SurnameFour");
            Assert.Throws<UserIsNotAssignedToThisScheduleItemException>(() => _EmployeeTask.RemoveEmployee(user4));
        }
        [Fact]
        public void RemoveEmployeesRange_UsersAssignedPreviously_ListShouldNotContainUsers()
        {
            Assert.True(_EmployeeTask._AssignedEmployees.Contains(_ApplicationUser) &&
                _EmployeeTask._AssignedEmployees.Contains(_ApplicationUser2) &&
                _EmployeeTask._AssignedEmployees.Contains(_ApplicationUser3));

            List<ApplicationUser> usersList = new List<ApplicationUser>() { _ApplicationUser, _ApplicationUser2, _ApplicationUser3 };
            _EmployeeTask.RemoveEmployeesRange(usersList);

            Assert.True(!_EmployeeTask._AssignedEmployees.Contains(_ApplicationUser) &&
                !_EmployeeTask._AssignedEmployees.Contains(_ApplicationUser2) &&
                !_EmployeeTask._AssignedEmployees.Contains(_ApplicationUser3));
        }

        [Fact]
        public void InvalidUpdateEndDate_EndDateIsBeforeStartDate_ShouldThrowEmployeeTaskEndDateCannotBeBeforeStartDate()
        {
            Assert.Throws<EndDateCannotBeBeforeStartDate>(() => _EmployeeTask.UpdateEndDate(_EmployeeTask._StartDate.Value.AddSeconds(-1)));
        }
        [Fact]
        public void ValidUpdateEndDate_EndDateShouldBeEqualToUpdatedDate()
        {
            DateTime dateToUpdate = _EmployeeTask._StartDate.Value.AddSeconds(1);
            _EmployeeTask.UpdateEndDate(dateToUpdate);
            Assert.Equal(dateToUpdate, _EmployeeTask._EndDate);
        }

        #endregion
    }
}
