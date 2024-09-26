using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.Models
{
    public class OrganizationReadModel : EntityId
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public ushort UserLimit { get; set; }
        public List<OfficeReadModel> Offices { get; set; }
        public List<EmployeeReadModel> Employees { get; set; }
        public SubscriptionReadModel Subscription { get; set; }
    }
}