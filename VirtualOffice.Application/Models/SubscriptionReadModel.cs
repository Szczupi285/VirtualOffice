using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.Models
{
    public class SubscriptionReadModel : EntityId
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Fee { get; set; }
        public bool IsPayed { get; set; }
    }
}