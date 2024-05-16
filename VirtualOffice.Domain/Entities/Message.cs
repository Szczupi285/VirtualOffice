using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.Message;

namespace VirtualOffice.Domain.Entities
{
    public class Message
    {
        public MessageId Id { get; }

        public ApplicationUser _Sender {  get; }

        public DateTime _SendDate 
        {
            get => DateTime.UtcNow;
        }

        public MessageContent _Content {get;}


        public Message(MessageId id, ApplicationUser sender, MessageContent content)
        {
            Id = id;
            _Sender = sender;
            _Content = content;
        }
    }
}
