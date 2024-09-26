using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.Models
{
    public class OfficeReadModel : EntityId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EmployeeReadModel> Employees { get; set; }
    }
}