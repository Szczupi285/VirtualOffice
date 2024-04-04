using System.ComponentModel.DataAnnotations;
using VirtualOffice.Domain.Exceptions.Office;
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

        private ICollection<ApplicationUser> _members;

        private Office(OfficeId id, OfficeName name, OfficeDescription description, Organization organization, ICollection<ApplicationUser> members) 
        { 
            Id= id;
            _officeName = name;
            _description = description;
            _organization = organization;
            _members = members;
        }   

        public void AddMember(ApplicationUser user) 
        {
            var alreadyExists = _members.Any(i => i.Id == user.Id);

            if (alreadyExists)
            {
                throw new UserIsAlreadyMemberOfThisOfficeException(user.Id);
            }
            _members.Add(user);
        }

        public void AddRangeMembers(ICollection<ApplicationUser> users)
        {
            foreach (var user in users) 
            { 
                AddMember(user);
            }
        }

        public void RemoveMember(ApplicationUser user) 
        {
            var alreadyExists = _members.Any(i => i.Id == user.Id);

            if (alreadyExists)
            {
                _members.Remove(user);
            }
            throw new UserIsAlreadyMemberOfThisOfficeException(user.Id);
        }

        public void RemoveRangeMembers(ICollection<ApplicationUser> users)
        {
            foreach(var user in users)
            {
                RemoveMember(user);
            }
        }

    }
    
}

