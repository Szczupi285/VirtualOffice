using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.Models
{
    public class MessageReadModel : EntityId
    {
        public string Id { get; set; }
        public EmployeeReadModel Sender { get; set; }
        public string Content { get; set; }
    }
}