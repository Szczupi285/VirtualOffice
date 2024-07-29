using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.Office;

namespace VirtualOffice.Application.DTO.Organization
{
    public class OrganizationOfficesDTO
    {
        public Guid Id { get; init; }
        public string _OrganizationName { get; init; }
        public List<OfficeIdAndNameDTO> _OfficesIdsAndNames { get; init; }
    }
}
