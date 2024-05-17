using DomainUnitTests.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Message;
using VirtualOffice.Domain.Exceptions.Note;
using VirtualOffice.Domain.ValueObjects.Message;
using VirtualOffice.Domain.ValueObjects.Note;
using VirtualOffice.Shared;

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

        #region CompareTo
        [Fact]
        public void CompareTo_EarlierSendDate_ReturnsNegative()
        {
            var sender1 = new ApplicationUser(Guid.NewGuid(), "name", "surname");
            var sender2 = new ApplicationUser(Guid.NewGuid(), "name", "surname");
            var message1 = new Message(Guid.NewGuid(), sender1, new MessageContent("Hello World 1"));
            Mock<IDateTimeProvider> mock = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddDays(-1));
            var message2 = new TestableMessage(Guid.NewGuid(), sender2, new MessageContent("Hello World 2"), mock.Object);
            Assert.True(message1.CompareTo(message2) == -1);
        }
        [Fact]
        public void CompareTo_EarlierSendDate_ReturnsPositive()
        {
            var sender1 = new ApplicationUser(Guid.NewGuid(), "name", "surname");
            var sender2 = new ApplicationUser(Guid.NewGuid(), "name", "surname");
            Mock<IDateTimeProvider> mock = new Mock<IDateTimeProvider>();
            Mock<IDateTimeProvider> mock1 = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddDays(-7));
            mock1.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddDays(-1));
            var message1 = new TestableMessage(Guid.NewGuid(), sender1, new MessageContent("Hello World 1"), mock.Object);
            var message2 = new TestableMessage(Guid.NewGuid(), sender2, new MessageContent("Hello World 2"), mock1.Object);

            Assert.True(message1.CompareTo(message2) == 1);
        }
        [Fact]
        public void CompareTo_EarlierSendDate_ReturnsZero()
        {
            var sender1 = new ApplicationUser(Guid.NewGuid(), "name", "surname");
            var sender2 = new ApplicationUser(Guid.NewGuid(), "name", "surname");
            Mock<IDateTimeProvider> mock = new Mock<IDateTimeProvider>();
            Mock<IDateTimeProvider> mock1 = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddDays(-7));
            var message1 = new TestableMessage(Guid.NewGuid(), sender1, new MessageContent("Hello World 1"), mock.Object);
            var message2 = new TestableMessage(Guid.NewGuid(), sender2, new MessageContent("Hello World 2"), mock.Object);

            Assert.True(message1.CompareTo(message2) == 0);
        }
        [Fact]
        public void CompareTo_Null_ShouldThrowArgumentNullException()
        {
            var sender1 = new ApplicationUser(Guid.NewGuid(), "name", "surname");
            var sender2 = new ApplicationUser(Guid.NewGuid(), "name", "surname");
            Mock<IDateTimeProvider> mock = new Mock<IDateTimeProvider>();
            Mock<IDateTimeProvider> mock1 = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddDays(-7));
            var message1 = new TestableMessage(Guid.NewGuid(), sender1, new MessageContent("Hello World 1"), mock.Object);
            var message2 = new TestableMessage(Guid.NewGuid(), sender2, new MessageContent("Hello World 2"), mock.Object);

            Assert.Throws<ArgumentNullException>(() => message1.CompareTo(null));
        }
        #endregion
    }
}
