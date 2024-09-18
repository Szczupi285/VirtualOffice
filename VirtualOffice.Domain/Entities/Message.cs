using VirtualOffice.Domain.ValueObjects.Message;
using VirtualOffice.Shared;

namespace VirtualOffice.Domain.Entities
{
    public class Message : IComparable<Message>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ApplicationUser _sender;
        private readonly MessageContent _content;

        public MessageId Id { get; }
        public ApplicationUser Sender => _sender;
        public DateTime SendDate => _dateTimeProvider.UtcNow();
        public MessageContent Content => _content;

        public Message(MessageId id, ApplicationUser sender, MessageContent content)
        {
            Id = id;
            _sender = sender;
            _content = content;
            _dateTimeProvider = new DateTimeProvider();
        }

        protected Message(MessageId id, ApplicationUser sender, MessageContent content, IDateTimeProvider dateTimeProvider)
        {
            Id = id;
            _sender = sender;
            _content = content;
            _dateTimeProvider = dateTimeProvider;
        }

        public int CompareTo(Message? other)
        {
            if (other == null)
                throw new ArgumentNullException();
            return other.SendDate.CompareTo(this.SendDate);
        }
    }
}