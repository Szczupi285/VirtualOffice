using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.Exceptions.Event;
using VirtualOffice.Domain.Exceptions.Note;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Event;
using VirtualOffice.Domain.ValueObjects.Note;

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
    }
}
