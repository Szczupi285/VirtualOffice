using VirtualOffice.Domain.Exceptions.ChatRoom;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;

namespace DomainUnitTests
{
    public class AbstractChatRoomValueObjectsUnitTest
    {
        #region ChatRoomId
        [Fact]
        public void EmptyChatRoomId_ShouldReturnEmptyChatRoomIdException()
        {
            Assert.Throws<EmptyChatRoomIdException>(()
                => new ChatRoomId(Guid.Empty));
        }
        [Fact]
        public void ValidChatRoomId_ValidGuidToChatRoomIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            ChatRoomId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidChatRoomId_ValidChatRoomIdToGuidConversionShouldEqual()
        {

            ChatRoomId id = new ChatRoomId(Guid.NewGuid());

            Guid guid = id;
            Assert.Equal(id.Value, guid);

        }
        #endregion
    }
}
