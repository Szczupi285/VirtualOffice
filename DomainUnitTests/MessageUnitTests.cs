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

        #region MessageContent
        [Fact]
        public void ValidMessageContent_ValidMessageContentToStringConversionShouldEqual()
        {
            MessageContent title = "example";
            string test = title;

            Assert.Equal(test, title);
        }
        [Fact]
        public void ValidMessageContent_StringToValidMessageContentConversionShouldEqual()
        {
            string test = "example";
            MessageContent title = test;
            Assert.Equal(test, title);
        }
        [Fact]
        public void NullMessageContent_ShouldThrowEmptyMessageContentException()
        {
            Assert.Throws<EmptyMessageContentException>(() => new MessageContent(null));
        }
        [Fact]
        public void EmptyMessageContent_ShouldThrowEmptyMessageContentException()
        {
            Assert.Throws<EmptyMessageContentException>(() => new MessageContent(""));
        }
        [Fact]
        public void TooLongMessageContent_ShouldThrowTooLongMessageContentException()
        {
            string invalidString = new string('a', 501);
            Assert.Throws<TooLongMessageContentException>(
                () => new MessageContent(invalidString));
        }
        [Fact]
        public void MaxCharactersMessageContent_ShouldNotThrowException()
        {
            string validString = new string('a', 500);
            new MessageContent(validString);
        }
        [Fact]
        public void MinCharactersMessageContent_ShouldNotThrowException()
        {
            string validString = new string('a', 1);
            new MessageContent(validString);

        }
        #endregion
    }
}
