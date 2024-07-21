using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Office;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.Interfaces
{
    public interface IOrganization
    {
        OrganizationName _name { get; }
        HashSet<Office> _offices { get; }
        HashSet<ApplicationUser> _organizationUsers { get; }
        ushort? _slotsLeft { get; }
        Subscription _subscription { get; }
        OrganizationUsedSlots _usedSlots { get; }
        OrganizationUserLimit _userLimit { get; }

        void AddOffice(Office office);
        void AddOfficeUser(ApplicationUser user, Office office);
        void AddRangeOfficeUsers(ICollection<ApplicationUser> users, Office office);
        void AddRangeUsers(ICollection<ApplicationUser> users);
        void AddUser(ApplicationUser user);
        Office GetOfficeById(OfficeId id);
        void RemoveOffice(Office office);
        void RemoveOfficeUser(ApplicationUser user, Office office);
        void RemoveRangeOfficeUsers(ICollection<ApplicationUser> users, Office office);
        void RemoveRangeUsers(ICollection<ApplicationUser> users);
        void RemoveUser(ApplicationUser user);
        void SetName(string name);
    }
}