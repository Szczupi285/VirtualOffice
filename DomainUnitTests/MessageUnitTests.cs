using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Message;
using VirtualOffice.Domain.Exceptions.Note;
using VirtualOffice.Domain.ValueObjects.Message;
using VirtualOffice.Domain.ValueObjects.Note;

namespace DomainUnitTests
{
    public class MessageUnitTests
    {
        #region MessageId
        [Fact]
        public void EmptyMessageId_ShouldReturnEmptyMessageIdException()
        {
            Assert.Throws<EmptyMessageIdException>(()
                => new MessageId(Guid.Empty));
        }
        [Fact]
        public void ValidMessageId_ValidGuidToMessageIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            MessageId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidMessageId_ValidMessageIdToGuidConversionShouldEqual()
        {

            MessageId id = new MessageId(Guid.NewGuid());

            Guid guid = id;
            Assert.Equal(id.Value, guid);

        }
        [Fact]
        public void ValidMessageId_GuidToValidMessageIdConversionShouldEqual()
        {

            MessageId id = new MessageId(Guid.NewGuid());

            Guid guid = id;

        }
        #endregion
    }
}
