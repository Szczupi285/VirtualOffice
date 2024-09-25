using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using VirtualOffice.Domain.ValueObjects.Message;
using VirtualOffice.Shared;

namespace VirtualOffice.Domain.Entities
{
    public class Message : IEquatable<Message>, IComparable<Message>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ApplicationUser _sender;
        private readonly MessageContent _content;

        public MessageId Id { get; }
        public ApplicationUser Sender => _sender;
        public DateTime SendDate { get; }
        public MessageContent Content => _content;

        public Message(MessageId id, ApplicationUser sender, MessageContent content)
        {
            Id = id;
            _sender = sender;
            _content = content;
            _dateTimeProvider = new DateTimeProvider();
            SendDate = _dateTimeProvider.UtcNow();
        }

        public Message(MessageId id, ApplicationUser sender, MessageContent content, DateTime sendDate)
        {
            Id = id;
            _sender = sender;
            _content = content;
            _dateTimeProvider = new DateTimeProvider();
            SendDate = sendDate;
        }

        protected Message(MessageId id, ApplicationUser sender, MessageContent content, IDateTimeProvider dateTimeProvider)
        {
            Id = id;
            _sender = sender;
            _content = content;
            _dateTimeProvider = dateTimeProvider;
            SendDate = _dateTimeProvider.UtcNow();
        }

        private Message()
        { }

        private static DateTime RoundToNearestSecond(DateTime dateTime)
        => new(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);

        public int CompareTo(Message? other)
        {
            ArgumentNullException.ThrowIfNull(other);

            DateTime thisRoundedDate = RoundToNearestSecond(this.SendDate);
            DateTime otherRoundedDate = RoundToNearestSecond(other.SendDate);

            int dateComparison = otherRoundedDate.CompareTo(thisRoundedDate);
            // we have to compare by Id, to threat Messages with same date as different.
            var value = dateComparison != 0 ? dateComparison : other.Id.Value.CompareTo(this.Id.Value);
            return value;
        }

        public bool Equals(Message other)
        {
            if (other == null) return false;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Message);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}