using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.ApplicationUser;

namespace VirtualOffice.Application.DTO.PublicChatRoom
{
    public class PublicChatRoomDTO
    {
        public Guid Id { get; init; }
        public string _Name { get; init; }
        public List<ApplicationUserDTO> _Participants { get; init; }
        public List<MessageDTO> _Messages { get; init; }
        
    }
}
