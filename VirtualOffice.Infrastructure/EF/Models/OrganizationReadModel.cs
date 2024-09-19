using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Infrastructure.Interfaces;

namespace VirtualOffice.Infrastructure.EF.Models
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