using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Event;
using VirtualOffice.Domain.ValueObjects.Event;

namespace DomainUnitTests
{
    public class CalendarEventUnitTests
    {
        public CalendarEvent _CalendarEvent { get; set; }
        public ApplicationUser User { get; set; }
        public CalendarEventUnitTests()
        {
            var eventId = new EventId(Guid.NewGuid());
            var title = new EventTitle("Sample Event");
            var startDate = new EventStartDate(DateTime.UtcNow);
            var endDate = new EventEndDate(DateTime.UtcNow.AddDays(1));
            var description = new EventDescription("This is a sample event description");
            User = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            var visibleTo = new List<ApplicationUser> { User };

            _CalendarEvent = new CalendarEvent(eventId, title, startDate, endDate, description, visibleTo);
        }

        #region EventId

        [Fact]
        public void EmptyEmptyEventIdId_ShouldReturnEmptyEmptyEventIdIdException()
        {
            Assert.Throws<EmptyEventIdException>(()
                => new EventId(Guid.Empty));
        }
        [Fact]
        public void ValidEmptyEventIdId_ValidGuidToAppUserIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            EventId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidEmptyEventIdId_ValidAppUserIdToGuidConversionShouldEqual()
        {

            EventId id = new EventId(Guid.NewGuid());

            Guid guid = id;

            Assert.Equal(id.Value, guid);
        }
        #endregion

        #region EventTitle
        [Fact]
        public void ValidEventTitle_ValidEventTitleToStringConversionShouldEqual()
        {
            EventTitle title = "example";
            string test = title;

            Assert.Equal(test, title);
        }
        [Fact]
        public void ValidEventTitle_StringToValidEventTitleConversionShouldEqual()
        {
            string test = "example";
            EventTitle title = test;
            Assert.Equal(test, title);
        }
        [Fact]
        public void NullEventTitle_ShouldThrowEmptyEventTitleException()
        {
            Assert.Throws<EmptyEventTitleException>(() => new EventTitle(null));
        }
        [Fact]
        public void EmptyEventTitle_ShouldThrowEmptyEventTitleException()
        {
            Assert.Throws<EmptyEventTitleException>(() => new EventTitle(""));
        }
        #endregion

        #region EventDescription

        [Fact]
        public void ValidEventDescription_ValidEventDescriptionToStringConversionShouldEqual()
        {
            EventDescription title = "example";
            string test = title;

            Assert.Equal(test, title);
        }
        [Fact]
        public void ValidEventDescription_StringToValidEventDescriptionConversionShouldEqual()
        {
            string test = "example";
            EventDescription title = test;
            Assert.Equal(test, title);
        }
        [Fact]
        public void NullEventDescription_ShouldThrowEmptyEventDescriptionException()
        {
            Assert.Throws<EmptyEventDescriptionException>(() => new EventDescription(null));
        }
        [Fact]
        public void EmptyEventDescription_ShouldThrowEmptyEventDescriptionException()
        {
            Assert.Throws<EmptyEventDescriptionException>(() => new EventDescription(""));
        }
        [Fact]
        public void TooLongEventDescription_ShouldThrowTooLongEventDescriptionException()
        {
            string invalidString = new string('a', 1001);
            Assert.Throws<TooLongEventDescriptionException>(
                () => new EventDescription(invalidString));
        }
        [Fact]
        public void MaxCharactersEventDescription_ShouldNotThrowException()
        {
            string validString = new string('a', 1000);
            new EventDescription(validString);
        }
        [Fact]
        public void MinCharactersEventDescription_ShouldNotThrowException()
        {
            string validString = new string('a', 1);
            new EventDescription(validString);

        }
        #endregion

        #region EventStartDate

        [Fact]
        public void EventStartDate31daysBeforeNow_EventStartDateCannotBePastException()
        {
            Assert.Throws<EventStartDateCannotBePastException>(()
                => new EventStartDate
                (
                    DateTime.UtcNow.AddDays(-31))
                );
        }
        [Fact]
        public void EventStartDate1HourBeforeNow_EventStartDateCannotBePastException()
        {
            Assert.Throws<EventStartDateCannotBePastException>(()
                => new EventStartDate
                (
                    DateTime.UtcNow.AddHours(-1))
                );
        }
        [Fact]
        public void EventStartDateValid31daysFromNow_ShouldNotThrowException()
        {
            EventStartDate startDate = DateTime.UtcNow.AddDays(31);
        }
        [Fact]
        public void EventStartDateValidYearFromNow_ShouldNotThrowException()
        {
            EventStartDate startDate = DateTime.UtcNow.AddYears(1);
        }
        [Fact]
        public void EventStartDateValidCurrentTime_ShouldNotThrowException()
        {
            EventStartDate startDate = DateTime.UtcNow;
        }

        [Fact]
        public void ValidData_DateTimeToEventStartDateConversion_ShouldEqual()
        {
            var dt = DateTime.UtcNow.AddDays(31);

            EventStartDate startDate = dt;

            Assert.Equal(dt, startDate);
        }
        [Fact]
        public void ValidData_EventStartDateToDateTimeConversionShouldEqual()
        {
            EventStartDate startDate = new EventStartDate(DateTime.UtcNow.AddDays(31));

            var dt = startDate;

            Assert.Equal(startDate, dt);
        }
        #endregion

        #region EventEndDate
        [Fact]
        public void EventEndDate1dayBeforeNow_EventEndDateCannotBePastException()
        {
            Assert.Throws<InvalidEventEndDateException>(()
                => new EventEndDate
                (
                    DateTime.UtcNow.AddDays(-1))
                );
        }
        [Fact]
        public void EventEndDate1dHourBeforeNow_EventEndDateCannotBePastException()
        {
            Assert.Throws<InvalidEventEndDateException>(()
                => new EventEndDate
                (
                    DateTime.UtcNow.AddHours(-1))
                );
        }
        [Fact]
        public void EventEndDateInvalidCurrentTime_ShouldThrowInvalidEventEndDateException()
        {
            Assert.Throws<InvalidEventEndDateException>(()
                => new EventEndDate
                (
                    DateTime.UtcNow
                ));
        }
        [Fact]
        public void EventEndDateValid31daysFromNow_ShouldNotThrowException()
        {
            EventEndDate EndDate = DateTime.UtcNow.AddDays(31);
        }
        [Fact]
        public void EventEndDateValidYearFromNow_ShouldNotThrowException()
        {
            EventEndDate EndDate = DateTime.UtcNow.AddYears(1);
        }


        [Fact]
        public void ValidData_DateTimeToEventEndDateConversion_ShouldEqual()
        {
            var dt = DateTime.UtcNow.AddDays(31);

            EventEndDate EndDate = dt;

            Assert.Equal(dt, EndDate);
        }
        [Fact]
        public void ValidData_EventEndDateToDateTimeConversionShouldEqual()
        {
            EventEndDate EndDate = new EventEndDate(DateTime.UtcNow.AddDays(31));


            var dt = EndDate;


            Assert.Equal(EndDate, dt);
        }
        #endregion

        #region Methods
        [Fact]
        public void EditTitle_SetsTitle()
        {
           
            string newTitle = "New Title";

            _CalendarEvent.EditTitle(newTitle);

            Assert.Equal(newTitle, _CalendarEvent._Title);
        }

        [Fact]
        public void EditDescription_SetsDescription()
        {
            string newDescription = "New Description";

            _CalendarEvent.EditDescription(newDescription);

            Assert.Equal(newDescription, _CalendarEvent._Description);
        }

        [Fact]
        public void EditStartDate_SetsStartDate()
        {
            DateTime startDate = DateTime.Now;

            _CalendarEvent.EditStartDate(startDate);

            Assert.Equal(startDate, _CalendarEvent._StartDate);
        }

        [Fact]
        public void EditEndDate_SetsEndDate()
        {
            DateTime endDate = DateTime.Now;

            _CalendarEvent.EditEndDate(endDate);

            Assert.Equal(endDate, _CalendarEvent._EndDate);
        }

        [Fact]
        public void AddUserToVisibleTo_AddsUser()
        {
            var userId = Guid.NewGuid();
            var user = new ApplicationUser(userId, "ExampleName", "ExampleSurname");

            _CalendarEvent.AddUserToVisibleTo(user);

            Assert.Contains(user, _CalendarEvent._VisibleTo);
        }

        [Fact]
        public void AddUserToVisibleTo_ThrowsExceptionIfUserAlreadyExists()
        {
            var userId = Guid.NewGuid();

            Assert.Throws<ThisEventIsAlreadyVIsibleToThisUser>(() => _CalendarEvent.AddUserToVisibleTo(User));
        }

        [Fact]
        public void AddUsersRangeToVisibleTo_AddsUsers()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname"),
                new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname")
           
            };

            _CalendarEvent.AddUsersRangeToVisibleTo(users);

            Assert.Equal(users.Count + 1, _CalendarEvent._VisibleTo.Count);
            foreach (var user in users)
            {
                Assert.Contains(user, _CalendarEvent._VisibleTo);
            }
        }
        [Fact]
        public void AddUsersRangeToVisibleTo_ShouldThrowThisEventIsAlreadyVisibleToThisUser()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname"),
                new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname"),
                User

            };
            
            Assert.Throws<ThisEventIsAlreadyVIsibleToThisUser>(() => _CalendarEvent.AddUsersRangeToVisibleTo(users));
        }

        [Fact]
        public void RemoveUserFromVisibleTo_RemovesUser()
        {
            _CalendarEvent.RemoveUserFromVisibleTo(User);

            Assert.DoesNotContain(User, _CalendarEvent._VisibleTo);
        }

        [Fact]
        public void RemoveUserFromVisibleTo_ThrowsExceptionIfUserNotFound()
        {
            var testUser = new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname");
            Assert.Throws<UserIsNotFoundInVisibleToCollection>(() => _CalendarEvent.RemoveUserFromVisibleTo(testUser));
        }

        [Fact]
        public void RemoveUsersRangeFromVisibleTo_RemovesUsers()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname"),
                new ApplicationUser(Guid.NewGuid(), "ExampleName", "ExampleSurname")

            };
            _CalendarEvent.AddUsersRangeToVisibleTo(users);

            _CalendarEvent.RemoveUsersRangeFromVisibleTo(users);

            foreach (var user in users)
            {
                Assert.DoesNotContain(user, _CalendarEvent._VisibleTo);
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

            Assert.Throws<UserIsNotFoundInVisibleToCollection>(() => _CalendarEvent.RemoveUsersRangeFromVisibleTo(users));
        }
        #endregion
    }
}
