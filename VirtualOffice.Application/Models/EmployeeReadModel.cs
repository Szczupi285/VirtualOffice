using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.Models
{
    public class EmployeeReadModel : EntityId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Permissions { get; set; }
    }
}