using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.Models
{
    public class PublicChatRoomReadModel : EntityId
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public List<EmployeeReadModel> Users { get; set; }
        public List<MessageReadModel> Messages { get; set; }
    }
}