using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Infrastructure.Interfaces;

namespace VirtualOffice.Infrastructure.EF.Models
{
    public class PrivateChatRoomReadModel : EntityId
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public List<EmployeeReadModel> Users { get; set; }
        public List<MessageReadModel> Messages { get; set; }
    }
}