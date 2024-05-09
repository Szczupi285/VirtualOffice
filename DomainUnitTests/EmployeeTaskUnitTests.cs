using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.EmployeeTask;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;
using VirtualOffice.Domain.Consts;

namespace DomainUnitTests
{
    public class EmployeeTaskUnitTests
    {
        private EmployeeTask _EmployeeTask { get; set; }

        public EmployeeTaskUnitTests()
        {

            ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "NameOne" , "SurnameOne");
            ApplicationUser user2 = new ApplicationUser(Guid.NewGuid(), "NameTwo" , "SurnameTwo");
            ApplicationUser user3 = new ApplicationUser(Guid.NewGuid(), "NameThree" , "SurnameThree");

            List<ApplicationUser> users = new List<ApplicationUser>();

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
            Assert.Throws<EmptyEmployeeTaskIdException>(()
                => new EmployeeTaskId(Guid.Empty));
        }
        [Fact]
        public void ValidEmployeeTaskId_ValidGuidToAppUserIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            EmployeeTaskId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidEmployeeTaskId_ValidAppUserIdToGuidConversionShouldEqual()
        {

            EmployeeTaskId id = new EmployeeTaskId(Guid.NewGuid());

            Guid guid = id;

            Assert.Equal(id.Value, guid);
        }
        #endregion

        #region EmployeeTaskTitle

        [Fact]
        public void EmptyEmployeeTaskTitle_ShouldReturnEmptyEmployeeTaskTitleException()
        {
            Assert.Throws<EmptyEmployeeTaskTitleException>(()
                => new EmployeeTaskTitle(""));
        }

        [Fact]
        public void NullEmployeeTaskTitle_ShouldReturnEmptyEmployeeTaskTitleException()
        {
            Assert.Throws<EmptyEmployeeTaskTitleException>(()
                => new EmployeeTaskTitle(null));
        }

        [Fact]
        public void ValidEmployeeTaskTitle_ValidStringToEmployeeTaskTitleConversion_ShouldEqual()
        {
            string title = "ExampleTitle";

            EmployeeTaskTitle tit = title;

            Assert.Equal(title, tit);
        }
        [Fact]
        public void ValidEmployeeTaskTitle_ValidEmployeeTaskTitleToStringConversionShouldEqual()
        {
            EmployeeTaskTitle tit = new EmployeeTaskTitle("ExampleTitle");

            string title = tit;

            Assert.Equal(tit, title);

        }
        [Fact]
        public void InvalidEmployeeTaskTitle_TooLongTitle()
        {
            string s = new string('a', 101);
            Assert.Throws<TooLongEmployeeTaskTitleException>(() => new EmployeeTaskTitle(s));
        }
        [Fact]
        public void ValidEmployeeTaskTitle_HundredChars()
        {
            string s = new string('a', 100);
            new EmployeeTaskTitle(s);
        }
        [Fact]
        public void ValidEmployeeTaskTitle_OneChar()
        {
            new EmployeeTaskTitle("a");
        }

        #endregion

        #region EmployeeTaskDescription 
        [Fact]
        public void ValidEmployeeTaskDescription_ValidStringToEmployeeTaskDescriptionConversion_ShouldEqual()
        {
            string Description = "ExampleDescription";

            EmployeeTaskDescription desc = Description;

            Assert.Equal(Description, desc);
        }
        [Fact]
        public void ValidEmployeeTaskDescription_ValidEmployeeTaskDescriptionToStringConversionShouldEqual()
        {
            EmployeeTaskDescription desc = new EmployeeTaskDescription("ExampleDescription");

            string Description = desc;

            Assert.Equal(desc, Description);

        }
        [Fact]
        public void InvalidEmployeeTaskDescription_TooLongDescription()
        {
            string s = new string('a', 1501);
            Assert.Throws<TooLongEmployeeTaskDescriptionException>(() => new EmployeeTaskDescription(s));
        }
        [Fact]
        public void ValidEmployeeTaskDescription_HundredChars()
        {
            string s = new string('a', 1500);
            new EmployeeTaskDescription(s);
        }
        [Fact]
        public void ValidEmployeeTaskDescription_OneChar()
        {
            new EmployeeTaskDescription("a");
        }
        [Fact]
        public void ValidEmployeeTaskDescription_WhiteSpace()
        {
            new EmployeeTaskDescription(" ");
        }
        [Fact]
        public void ValidEmployeeTaskDescription_Empty()
        {
            new EmployeeTaskDescription("");
        }
        #endregion

        #region EmployeeTaskStartDate

        [Fact]
        public void EmployeeTaskStartDate31daysBeforeNow_EmployeeTaskStartDateCannotBePastException()
        {
            Assert.Throws<EmployeeTaskStartDateCannotBePastException>(()
                => new EmployeeTaskStartDate
                (
                    DateTime.UtcNow.AddDays(-31))
                );
        }
        [Fact]
        public void EmployeeTaskStartDateValid31daysFromNow_ShouldNotThrowException()
        {
            EmployeeTaskStartDate startDate = DateTime.UtcNow.AddDays(31);
        }
        [Fact]
        public void EmployeeTaskStartDateValidYearFromNow_ShouldNotThrowException()
        {
            EmployeeTaskStartDate startDate = DateTime.UtcNow.AddYears(1);
        }
        [Fact]
        public void EmployeeTaskStartDateValidCurrentTime_ShouldNotThrowException()
        {
            EmployeeTaskStartDate startDate = DateTime.UtcNow;
        }

        [Fact]
        public void ValidData_DateTimeToEmployeeTaskStartDateConversion_ShouldEqual()
        {
            var dt = DateTime.UtcNow.AddDays(31);

            EmployeeTaskStartDate startDate = dt;

            Assert.Equal(dt, startDate);
        }
        [Fact]
        public void ValidData_EmployeeTaskStartDateToDateTimeConversionShouldEqual()
        {
            EmployeeTaskStartDate startDate = new EmployeeTaskStartDate(DateTime.UtcNow.AddDays(31));


            var dt = startDate;


            Assert.Equal(startDate, dt);
        }


        #endregion

        #region EmployeeTaskEndDate

        [Fact]
        public void EmployeeTaskEndDate31daysBeforeNow_EmployeeTaskEndDateCannotBePastException()
        {
            Assert.Throws<InvalidEmployeeTaskEndDateException>(()
                => new EmployeeTaskEndDate
                (
                    DateTime.UtcNow.AddDays(-1))
                );
        }
        [Fact]
        public void EmployeeTaskEndDateInvalidCurrentTime_ShouldThrowInvalidEmployeeTaskEndDateException()
        {
            Assert.Throws<InvalidEmployeeTaskEndDateException>(()
                => new EmployeeTaskEndDate
                (
                    DateTime.UtcNow
                ));
        }
        [Fact]
        public void EmployeeTaskEndDateValid31daysFromNow_ShouldNotThrowException()
        {
            EmployeeTaskEndDate EndDate = DateTime.UtcNow.AddDays(31);
        }
        [Fact]
        public void EmployeeTaskEndDateValidYearFromNow_ShouldNotThrowException()
        {
            EmployeeTaskEndDate EndDate = DateTime.UtcNow.AddYears(1);
        }


        [Fact]
        public void ValidData_DateTimeToEmployeeTaskEndDateConversion_ShouldEqual()
        {
            var dt = DateTime.UtcNow.AddDays(31);

            EmployeeTaskEndDate EndDate = dt;

            Assert.Equal(dt, EndDate);
        }
        [Fact]
        public void ValidData_EmployeeTaskEndDateToDateTimeConversionShouldEqual()
        {
            EmployeeTaskEndDate EndDate = new EmployeeTaskEndDate(DateTime.UtcNow.AddDays(31));


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
        public void EmployeeTask_SetStatus_ProperlySetted()
        {
            _EmployeeTask.SetStatus(EmployeeTaskStatusEnum.InProgress);
            Assert.Equal(EmployeeTaskStatusEnum.InProgress, _EmployeeTask._TaskStatus);
        }


        
        #endregion
    }
}
