using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Infrastructure.Interfaces;

namespace VirtualOffice.Infrastructure.EF.Models
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