using System.ComponentModel.DataAnnotations;
using VirtualOffice.Domain.Exceptions.Office;
using VirtualOffice.Domain.ValueObjects.Office;

namespace VirtualOffice.Domain.Entities
{
    public class Office
    {
        public OfficeId Id { get; private set; }

        public OfficeName _officeName { get; private set; }

        public OfficeDescription _description { get; private set; }

        // private ActivityLog _activityLog;
        
        // unlike in Organization situation, Office might not have any members/users assigned to it
        internal ICollection<ApplicationUser> _members { get; private set; }

        internal Office(OfficeId id, OfficeName name, OfficeDescription description, ICollection<ApplicationUser> members) 
        { 
            Id= id;
            _officeName = name;
            _description = description;
           
            _members = members;
        }   

        public void AddMember(ApplicationUser user) 
        {
            bool alreadyExists = _members.Any(i => i.Id == user.Id);

            if (alreadyExists)
            {
                throw new UserIsAlreadyMemberOfThisOfficeException(user.Id);
            }
            _members.Add(user);
        }

        public void AddRangeMembers(ICollection<ApplicationUser> users)
        {
            foreach (ApplicationUser user in users) 
            { 
                AddMember(user);
            }
        }

        public void RemoveMember(ApplicationUser user) 
        {
            bool alreadyExists = _members.Any(i => i.Id == user.Id);

            if (alreadyExists)
            {
                _members.Remove(user);
            }
            else
                throw new UserIsNotMemberOfThisOfficeException(user.Id);
        }

        public void RemoveRangeMembers(ICollection<ApplicationUser> users)
        {
            foreach(ApplicationUser user in users)
            {
                RemoveMember(user);
            }
        }

    }
    
}

