using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.Message;
using VirtualOffice.Shared;

namespace VirtualOffice.Domain.Entities
{
    public class Message : IComparable<Message>
    {
        private readonly IDateTimeProvider _DateTimeProvider;
        public MessageId Id { get; }

        public ApplicationUser _Sender {  get; }

        public DateTime _SendDate 
        {
            get => _DateTimeProvider.UtcNow();
        }

        public MessageContent _Content { get; }


        public Message(MessageId id, ApplicationUser sender, MessageContent content)
        {
            Id = id;
            _Sender = sender;
            _Content = content;
            _DateTimeProvider = new DateTimeProvider();
        }
        protected Message(MessageId id, ApplicationUser sender, MessageContent content, IDateTimeProvider dateTimeProvider)
        {
            Id = id;
            _Sender = sender;
            _Content = content;
            _DateTimeProvider = dateTimeProvider;
        }

        public int CompareTo(Message? other)
        {
            if (other == null)
                throw new ArgumentNullException();
            return other._SendDate.CompareTo(this._SendDate);
        }

       
    }
}
