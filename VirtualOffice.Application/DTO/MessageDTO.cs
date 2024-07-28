using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Application.DTO
{
    public class MessageDTO
    {
        public Guid MessageId { get; init; }
        public Guid SenderID { get; init; }
        public string Message { get; init; }
        public DateTime SendDate { get; init; }
    }
}
