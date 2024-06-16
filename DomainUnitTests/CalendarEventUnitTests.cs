using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.DomainEvents.CalendarEventEvents;
using VirtualOffice.Domain.DomainEvents.ScheduleItem;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;

namespace DomainUnitTests
{
    public class CalendarEventUnitTests
    {
        public CalendarEvent _CalendarEvent { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser UserNotAdded { get; set; }

        public CalendarEventUnitTests()
        {
            var eventId = new ScheduleItemId(Guid.NewGuid());
            var title = new ScheduleItemTitle("Sample Event");
            var startDate = new ScheduleItemStartDate(DateTime.UtcNow);
            var endDate = new ScheduleItemEndDate(DateTime.UtcNow.AddDays(1));
            var description = new ScheduleItemDescription("This is a sample event description");
            User = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            UserNotAdded = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
            var visibleTo = new HashSet<ApplicationUser> { User };

            _CalendarEvent = new CalendarEvent(eventId, title, description, visibleTo, startDate, endDate);
        }

        #region Events
        [Fact]
        public void SetTitle_ShouldRaiseScheduleItemTitleSetted()
        {
            _CalendarEvent.SetTitle("Title");
            var Event = _CalendarEvent.Events.OfType<ScheduleItemTitleSetted>().Single();
        }
        [Fact]
        public void SetTitle_ShouldRaiseScheduleItemTitleSetted_CalendarEventShouldEqual()
        {
            _CalendarEvent.SetTitle("Title");
            var Event = _CalendarEvent.Events.OfType<ScheduleItemTitleSetted>().Single();
            Assert.Equal(_CalendarEvent ,Event.abstractScheduleItem);
        }
        [Fact]
        public void SetTitle_ShouldRaiseScheduleItemTitleSetted_TitleShouldEqual()
        {
            _CalendarEvent.SetTitle("Title");
            var Event = _CalendarEvent.Events.OfType<ScheduleItemTitleSetted>().Single();
            Assert.Equal("Title", Event.title);
        }

        [Fact]
        public void SetDescription_ShouldRaiseScheduleItemDescriptionSetted()
        {
            _CalendarEvent.SetDescription("Description");
            var Event = _CalendarEvent.Events.OfType<ScheduleItemDescriptionSetted>().Single();
        }
        [Fact]
        public void SetDescription_ShouldRaiseScheduleItemDescriptionSetted_CalendarEventShouldEqual()
        {
            _CalendarEvent.SetDescription("Description");
            var Event = _CalendarEvent.Events.OfType<ScheduleItemDescriptionSetted>().Single();
            Assert.Equal(_CalendarEvent, Event.abstractScheduleItem);
        }
        [Fact]
        public void SetDescription_ShouldRaiseScheduleItemDescriptionSetted_DescriptionShouldEqual()
        {
            _CalendarEvent.SetDescription("Description");
            var Event = _CalendarEvent.Events.OfType<ScheduleItemDescriptionSetted>().Single();
            Assert.Equal("Description", Event.description);
        }
        [Fact]
        public void AddEmployee_ShouldNotRaiseEmployeeAddedToScheduleItem()
        {
            _CalendarEvent.AddEmployee(User);
            var Event = _CalendarEvent.Events.OfType<EmployeeAddedToScheduleItem>().SingleOrDefault();
            Assert.Null(Event);
        }
        [Fact]
        public void AddEmployee_ShouldRaiseEmployeeAddedToScheduleItem()
        {
            _CalendarEvent.AddEmployee(UserNotAdded);
            var Event = _CalendarEvent.Events.OfType<EmployeeAddedToScheduleItem>().Single();
        }
        [Fact]
        public void AddEmployee_ShouldRaiseEmployeeAddedToScheduleItem_CalendarEventShouldEqual()
        {
            _CalendarEvent.AddEmployee(UserNotAdded);
            var Event = _CalendarEvent.Events.OfType<EmployeeAddedToScheduleItem>().Single();
            Assert.Equal(_CalendarEvent, Event.abstractScheduleItem);
        }
        [Fact]
        public void AddEmployee_ShouldRaiseEmployeeAddedToScheduleItem_UserShouldEqual()
        {
            _CalendarEvent.AddEmployee(UserNotAdded);
            var Event = _CalendarEvent.Events.OfType<EmployeeAddedToScheduleItem>().Single();
            Assert.Equal(UserNotAdded, Event.user);
        }
        [Fact]
        public void RemoveEmployee_ShouldRaiseEmployeeRemoverFromScheduleItem()
        {
            _CalendarEvent.RemoveEmployee(User);
            var Event = _CalendarEvent.Events.OfType<EmployeeRemovedFromScheduleItem>().Single();
        }
        [Fact]
        public void RemoveEmployee_ShouldRaiseEmployeeRemoverFromScheduleItem_CalendarEventShouldEqual()
        {
            _CalendarEvent.RemoveEmployee(User);
            var Event = _CalendarEvent.Events.OfType<EmployeeRemovedFromScheduleItem>().Single();
            Assert.Equal(_CalendarEvent, Event.abstractScheduleItem);
        }
        [Fact]
        public void RemoveEmployee_ShouldRaiseEmployeeRemoverFromScheduleItem_UserShouldEqual()
        {
            _CalendarEvent.RemoveEmployee(User);
            var Event = _CalendarEvent.Events.OfType<EmployeeRemovedFromScheduleItem>().Single();
            Assert.Equal(User, Event.user);
        }
        [Fact]
        public void UpdateEndDate_ShouldRaiseScheduleItemEndDateUpdate()
        {
            DateTime date = DateTime.UtcNow.AddHours(5);
            _CalendarEvent.UpdateEndDate(date);
            var Event = _CalendarEvent.Events.OfType<ScheduleItemEndDateUpdated>().Single();
        }
        [Fact]
        public void UpdateEndDate_ShouldRaiseScheduleItemEndDateUpdate_CalendarEventShouldEqual()
        {
            DateTime date = DateTime.UtcNow.AddHours(5);
            _CalendarEvent.UpdateEndDate(date);
            var Event = _CalendarEvent.Events.OfType<ScheduleItemEndDateUpdated>().Single();
            Assert.Equal(_CalendarEvent, Event.abstractScheduleItem);
        }
        [Fact]
        public void UpdateEndDate_ShouldRaiseScheduleItemEndDateUpdate_DateShouldEqual()
        {
            DateTime date = DateTime.UtcNow.AddHours(5);
            _CalendarEvent.UpdateEndDate(date);
            var Event = _CalendarEvent.Events.OfType<ScheduleItemEndDateUpdated>().Single();
            Assert.Equal(date, Event.EndDate);
        }
        [Fact]
        public void UpdateStartDate_ShouldRaiseCalendarEventStartDateUpdate()
        {
            DateTime date = DateTime.UtcNow.AddHours(5);
            _CalendarEvent.UpdateStartDate(date);
            var Event = _CalendarEvent.Events.OfType<CalendarEventStartDateUpdated>().Single();
        }
        [Fact]
        public void UpdateStartDate_ShouldRaiseCalendarEventStartDateUpdate_CalendarEventShouldEqual()
        {
            DateTime date = DateTime.UtcNow.AddHours(5);
            _CalendarEvent.UpdateStartDate(date);
            var Event = _CalendarEvent.Events.OfType<CalendarEventStartDateUpdated>().Single();
            Assert.Equal(_CalendarEvent, Event.calendarEvent);
        }
        [Fact]
        public void UpdateStartDate_ShouldRaiseCalendarEventStartDateUpdate_DateShouldEqual()
        {
            DateTime date = DateTime.UtcNow.AddHours(5);
            _CalendarEvent.UpdateStartDate(date);
            var Event = _CalendarEvent.Events.OfType<CalendarEventStartDateUpdated>().Single();
            Assert.Equal(date, Event.StartDate);
        }
        #endregion

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

        #region Methods
        [Fact]
        public void EditTitle_SetsTitle()
        {
           
            string newTitle = "New Title";

            _CalendarEvent.SetTitle(newTitle);

            Assert.Equal(newTitle, _CalendarEvent._Title);
        }

        [Fact]
        public void EditDescription_SetsDescription()
        {
            string newDescription = "New Description";

            _CalendarEvent.SetDescription(newDescription);

            Assert.Equal(newDescription, _CalendarEvent._Description);
        }

        [Fact]
        public void EditStartDate_SetsStartDate()
        {
            DateTime startDate = DateTime.Now;

            _CalendarEvent.UpdateStartDate(startDate);

            Assert.Equal(startDate, _CalendarEvent._StartDate);
        }

        [Fact]
        public void EditEndDate_SetsEndDate()
        {
            DateTime endDate = DateTime.Now;

            _CalendarEvent.UpdateEndDate(endDate);

            Assert.Equal(endDate, _CalendarEvent._EndDate);
        }

        [Fact]
        public void AddUserToVisibleTo_AddsUser()
        {
            var userId = Guid.NewGuid();
            var user = new ApplicationUser(userId, "ExampleName", "ExampleSurname");

            _CalendarEvent.AddEmployee(user);

            Assert.Contains(user, _CalendarEvent._AssignedEmployees);
        }

        [Fact]
        public void AddUsersRangeToVisibleTo_AddsUsers()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname"),
                new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname")
           
            };

            _CalendarEvent.AddEmployeesRange(users);

            Assert.Equal(users.Count + 1, _CalendarEvent._AssignedEmployees.Count);
            foreach (var user in users)
            {
                Assert.Contains(user, _CalendarEvent._AssignedEmployees);
            }
        }

        [Fact]
        public void RemoveUserFromVisibleTo_RemovesUser()
        {
            _CalendarEvent.RemoveEmployee(User);

            Assert.DoesNotContain(User, _CalendarEvent._AssignedEmployees);
        }

        [Fact]
        public void RemoveUserFromVisibleTo_ThrowsExceptionIfUserNotFound()
        {
            var testUser = new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname");
            Assert.Throws<UserIsNotAssignedToThisScheduleItemException>(() => _CalendarEvent.RemoveEmployee(testUser));
        }

        [Fact]
        public void RemoveUsersRangeFromVisibleTo_RemovesUsers()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname"),
                new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname")

            };
            _CalendarEvent.AddEmployeesRange(users);
          
            _CalendarEvent.RemoveEmployeesRange(users);

            foreach (var user in users)
            {
                Assert.DoesNotContain(user, _CalendarEvent._AssignedEmployees);
                Assert.Contains(User, _CalendarEvent._AssignedEmployees);
            }
        }

        [Fact]
        public void RemoveUsersRangeFromVisibleTo_ShouldThrowUserIsNotFoundInVisibleToCollection()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname"),
                new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname"),
                User

            };

            Assert.Throws<UserIsNotAssignedToThisScheduleItemException>(() => _CalendarEvent.RemoveEmployeesRange(users));
        }
        #endregion
    }
}
