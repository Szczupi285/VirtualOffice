using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Application.Models
{
    public class EmployeeReadModel : EntityId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public PermissionsEnum Permissions { get; set; }
    }
}