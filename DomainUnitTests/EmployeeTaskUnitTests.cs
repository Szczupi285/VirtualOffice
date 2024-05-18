using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;
using VirtualOffice.Domain.Consts;

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


        #region EmployeeTaskId

        [Fact]
        public void EmptyEmployeeTaskId_ShouldReturnEmptyEmployeeTaskIdException()
        {
            Assert.Throws<EmptyEmployeeScheduleItemIdException>(()
                => new ScheduleItemId(Guid.Empty));
        }
        [Fact]
        public void ValidEmployeeTaskId_ValidGuidToAppUserIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            ScheduleItemId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidEmployeeTaskId_ValidAppUserIdToGuidConversionShouldEqual()
        {

            ScheduleItemId id = new ScheduleItemId(Guid.NewGuid());

            Guid guid = id;

            Assert.Equal(id.Value, guid);
        }
        #endregion

        #region EmployeeTaskTitle

        [Fact]
        public void EmptyEmployeeTaskTitle_ShouldReturnEmptyEmployeeTaskTitleException()
        {
            Assert.Throws<EmptyScheduleItemTitleException>(()
                => new ScheduleItemTitle(""));
        }

        [Fact]
        public void NullEmployeeTaskTitle_ShouldReturnEmptyEmployeeTaskTitleException()
        {
            Assert.Throws<EmptyScheduleItemTitleException>(()
                => new ScheduleItemTitle(null));
        }

        [Fact]
        public void ValidEmployeeTaskTitle_ValidStringToEmployeeTaskTitleConversion_ShouldEqual()
        {
            string title = "ExampleTitle";

            ScheduleItemTitle tit = title;

            Assert.Equal(title, tit);
        }
        [Fact]
        public void ValidEmployeeTaskTitle_ValidEmployeeTaskTitleToStringConversionShouldEqual()
        {
            ScheduleItemTitle tit = new ScheduleItemTitle("ExampleTitle");

            string title = tit;

            Assert.Equal(tit, title);

        }
        [Fact]
        public void InvalidEmployeeTaskTitle_TooLongTitle()
        {
            string s = new string('a', 101);
            Assert.Throws<TooLongScheduleItemTitleException>(() => new ScheduleItemTitle(s));
        }
        [Fact]
        public void ValidEmployeeTaskTitle_HundredChars()
        {
            string s = new string('a', 100);
            new ScheduleItemTitle(s);
        }
        [Fact]
        public void ValidEmployeeTaskTitle_OneChar()
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

        #region EmployeeTaskStartDate

        [Fact]
        public void EmployeeTaskStartDate31daysBeforeNow_EmployeeTaskStartDateCannotBePastException()
        {
            Assert.Throws<ScheduleItemStartDateCannotBePastException>(()
                => new ScheduleItemStartDate
                (
                    DateTime.UtcNow.AddDays(-31))
                );
        }
        [Fact]
        public void EmployeeTaskStartDateValid31daysFromNow_ShouldNotThrowException()
        {
            ScheduleItemStartDate startDate = DateTime.UtcNow.AddDays(31);
        }
        [Fact]
        public void EmployeeTaskStartDateValidYearFromNow_ShouldNotThrowException()
        {
            ScheduleItemStartDate startDate = DateTime.UtcNow.AddYears(1);
        }
        [Fact]
        public void EmployeeTaskStartDateValidCurrentTime_ShouldNotThrowException()
        {
            ScheduleItemStartDate startDate = DateTime.UtcNow;
        }

        [Fact]
        public void ValidData_DateTimeToEmployeeTaskStartDateConversion_ShouldEqual()
        {
            var dt = DateTime.UtcNow.AddDays(31);

            ScheduleItemStartDate startDate = dt;

            Assert.Equal(dt, startDate);
        }
        [Fact]
        public void ValidData_EmployeeTaskStartDateToDateTimeConversionShouldEqual()
        {
            ScheduleItemStartDate startDate = new ScheduleItemStartDate(DateTime.UtcNow.AddDays(31));


            var dt = startDate;


            Assert.Equal(startDate, dt);
        }


        #endregion

        #region EmployeeTaskEndDate

        [Fact]
        public void EmployeeTaskEndDate1dayBeforeNow_EmployeeTaskEndDateCannotBePastException()
        {
            Assert.Throws<InvalidScheduleItemEndDateException>(()
                => new ScheduleItemEndDate
                (
                    DateTime.UtcNow.AddDays(-1))
                );
        }
        [Fact]
        public void EmployeeTaskEndDateInvalidCurrentTime_ShouldThrowInvalidEmployeeTaskEndDateException()
        {
            Assert.Throws<InvalidScheduleItemEndDateException>(()
                => new ScheduleItemEndDate
                (
                    DateTime.UtcNow
                ));
        }
        [Fact]
        public void EmployeeTaskEndDateValid31daysFromNow_ShouldNotThrowException()
        {
            ScheduleItemEndDate EndDate = DateTime.UtcNow.AddDays(31);
        }
        [Fact]
        public void EmployeeTaskEndDateValidYearFromNow_ShouldNotThrowException()
        {
            ScheduleItemEndDate EndDate = DateTime.UtcNow.AddYears(1);
        }


        [Fact]
        public void ValidData_DateTimeToEmployeeTaskEndDateConversion_ShouldEqual()
        {
            var dt = DateTime.UtcNow.AddDays(31);

            ScheduleItemEndDate EndDate = dt;

            Assert.Equal(dt, EndDate);
        }
        [Fact]
        public void ValidData_EmployeeTaskEndDateToDateTimeConversionShouldEqual()
        {
            ScheduleItemEndDate EndDate = new ScheduleItemEndDate(DateTime.UtcNow.AddDays(31));


            var dt = EndDate;


            Assert.Equal(EndDate, dt);
        }

        #endregion

        #region Properties
        [Fact]
        public void EmployeeTaskStatus_DefaultStatus_ShouldBeNotStarted()
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
        public void AddEmployee_EmployeeIsAlreadyAssigned_ShouldThrowUserIsAlreadyMemberOfThisCollectionException()
        {
            Assert.Throws<UserIsAlreadyMemberOfThisCollectionException>(() => _EmployeeTask.AddEmployee(_ApplicationUser));
        }
        [Fact]
        public void AddEmployee_UserNotAssignedPreviously_ListShouldContainUser()
        {
            ApplicationUser user4 = new ApplicationUser(Guid.NewGuid(), "NameFour", "SurnameFour");
            _EmployeeTask.AddEmployee(user4);
            Assert.True(_EmployeeTask._AssignedEmployees.Contains(user4));
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
            Assert.True(_EmployeeTask._AssignedEmployees.Contains(_ApplicationUser));
            _EmployeeTask.RemoveEmployee(_ApplicationUser);
            Assert.False(_EmployeeTask._AssignedEmployees.Contains(_ApplicationUser));
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
