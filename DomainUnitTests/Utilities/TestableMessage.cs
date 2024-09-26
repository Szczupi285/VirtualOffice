using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Message;
using VirtualOffice.Shared;

namespace DomainUnitTests.Utilities
{
    internal class TestableMessage : Message
    {
        public TestableMessage(MessageId id, ApplicationUser sender, MessageContent content, IDateTimeProvider dateTimeProvider) : base(id, sender, content, dateTimeProvider)
        {

        }
    }
}
