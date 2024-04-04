using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VirtualOffice.Domain.Exceptions.Organization;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.Entities
{

    public class Organization 
    {
        public OrganizationId Id { get; private set; }

        public OrganizationName _name { get; private set; }

        //rethink
        public OrganizationUserLimit _userLimit { get => (ushort)_subscription._subType; }

        public OrganizationUsedSlots _usedSlots { get; private set; }

        public ICollection<Office> _offices { get; private set; }

        // consider this relation since office already contains 
        // users list, but we would have to check distinct users every time
        // we add user to office. Since one user may be a member of many offices
        public ICollection<ApplicationUser> _organizationUsers { get; private set; }

        public Subscription _subscription { get; private set; }

        private bool _isUnlimited;

        internal Organization(OrganizationId id, OrganizationName name, OrganizationUsedSlots usedSlots,
             ICollection<Office> offices, ICollection<ApplicationUser> organizationUsers
            ,Subscription subscription, bool isUnlimited)
        {
            Id = id;
            _name = name;
            _usedSlots = usedSlots;
            _offices = offices;
            _organizationUsers = organizationUsers;
            _subscription = subscription;
            _isUnlimited = isUnlimited;
        }
         
        public void AddUser(ApplicationUser user)
        {
            bool aleadyExists = _organizationUsers.Any(u => u.Id == user.Id);

            if (aleadyExists)
                throw new UserIsAlreadyMemberOfThisOrganizationException(user.Id);
            else if (!_isUnlimited && _usedSlots >= _userLimit)
                throw new OrganizationNotEnoughSlotsException();
            
            _organizationUsers.Add(user);
            _usedSlots++;
        }
        public void AddRangeUsers(ICollection<ApplicationUser> users)
        {
            foreach (ApplicationUser user in users)
            {
                AddUser(user);
            }
        }

        public void RemoveUser(ApplicationUser user) 
        { 
            bool aleadyExists = _organizationUsers.Any(u => u.Id == user.Id);

            if (aleadyExists)
                _organizationUsers.Remove(user);
            else
                throw new UserIsNotAMemberOfThisOrganization(user.Id);
        }
        public void RemoveRangeMembers(ICollection<ApplicationUser> users)
        {
            foreach (ApplicationUser user in users)
            {
                RemoveUser(user);
            }
        }





    }
}
