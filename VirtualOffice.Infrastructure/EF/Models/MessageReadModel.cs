using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Infrastructure.Interfaces;

namespace VirtualOffice.Infrastructure.EF.Models
{
    public class MessageReadModel : EntityId
    {
        public string Id { get; set; }
        public UserReadModel Sender { get; set; }
        public string Content { get; set; }
    }
}