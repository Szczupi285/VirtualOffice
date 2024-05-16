using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Event;
using VirtualOffice.Domain.ValueObjects.Event;

namespace DomainUnitTests
{
    public class CalendarEventUnitTests
    {
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
    }
}
