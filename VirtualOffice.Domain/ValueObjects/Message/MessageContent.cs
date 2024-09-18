using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Message;

namespace VirtualOffice.Domain.ValueObjects.Message
{
    public sealed record MessageContent : AbstractRecordName
    {
        public MessageContent(string value) : base(value, 500, new EmptyMessageContentException(), new TooLongMessageContentException(value, 500))
        {
        }

        public static implicit operator MessageContent(string content)
            => new(content);
    }
}