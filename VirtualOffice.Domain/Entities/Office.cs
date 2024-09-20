using VirtualOffice.Domain.Exceptions.Office;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Office;

namespace VirtualOffice.Domain.Entities
{
    public class Office
    {
        public OfficeId Id { get; }

        public OfficeName _officeName { get; private set; }

        public OfficeDescription _description { get; private set; }

        // private ActivityLog _activityLog;

        // unlike in Organization situation, Office might not have any members/users assigned to it
        public HashSet<ApplicationUser> _members { get; private set; }

        public Office(OfficeId id, OfficeName name, OfficeDescription description, HashSet<ApplicationUser> members)
        {
            Id = id;
            _officeName = name;
            _description = description;

            _members = members;
        }

        private Office()
        { }

        public void SetName(OfficeName name) => _officeName = name;

        public void SetDescription(OfficeDescription description) => _description = description;

        internal bool AddMember(ApplicationUser user)
        {
            bool alreadyExists = _members.Any(i => i.Id == user.Id);

            if (alreadyExists)
                return false;

            _members.Add(user);
            return true;
        }

        internal bool RemoveMember(ApplicationUser user)
        {
            bool alreadyExists = _members.Any(i => i.Id == user.Id);

            if (!alreadyExists)
                return false;

            _members.Remove(user);
            return true;
        }

        public ApplicationUser GetMemberById(ApplicationUserId id)
            => _members.FirstOrDefault(u => u.Id == id) ?? throw new OfficeMemberNotFoundException(id.ToString());

        public ApplicationUser GetMemberBySurname(ApplicationUserSurname surname)
            => _members.FirstOrDefault(u => u._Surname == surname) ?? throw new OfficeMemberNotFoundException(surname);

        public ICollection<ApplicationUser> GetAllMembers() => _members;
    }
}