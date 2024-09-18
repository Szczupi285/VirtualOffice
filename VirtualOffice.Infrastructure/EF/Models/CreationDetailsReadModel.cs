using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Infrastructure.Interfaces;

namespace VirtualOffice.Infrastructure.EF.Models
{
    public class CreationDetailsReadModel
    {
        public string UserId { get; set; }
        public DateTime DocumentCreationDate { get; set; }
    }
}