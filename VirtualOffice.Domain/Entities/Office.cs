using System.ComponentModel.DataAnnotations;
using VirtualOffice.Domain.ValueObjects.Office;

namespace VirtualOffice.Domain.Entities
{
    public class Office
    {
        public OfficeId Id { get; private set; }

        private OfficeName _officeName;

        private OfficeDescription _description;

        // private ActivityLog _activityLog;

        private Organization _organization;

        private ICollection<ApplicationUser> members;
    }
}

