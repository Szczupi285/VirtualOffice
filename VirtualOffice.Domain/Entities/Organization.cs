using VirtualOffice.Domain.Exceptions.Organization;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.Entities
{

    public class Organization 
    {
        public OrganizationId Id { get; }

        public OrganizationName _name { get; private set; }

        public OrganizationUserLimit _userLimit {
            get
            {
                if(_subscription._subType == Consts.SubscriptionTypeEnum.Unlimited)
                {
                    return null!;
                }
                return (ushort?)_subscription._subType;
            }
        }  

        public OrganizationUsedSlots _usedSlots { get => (uint)_organizationUsers.Count(); } 

        public ICollection<Office> _offices { get; private set; }

        // consider this relation since office already contains 
        // users list, but we would have to check distinct users every time
        // we add user to office. Since one user may be a member of many offices
        public ICollection<ApplicationUser> _organizationUsers { get; private set; }

        public Subscription _subscription { get; private set; }


        private bool _isUnlimited 
        {
            get
            {
                if(_subscription._subType == Consts.SubscriptionTypeEnum.Unlimited)
                    return true;
                return false;
            }
        }

        internal Organization(OrganizationId id, OrganizationName name,
             ICollection<Office> offices, ICollection<ApplicationUser> organizationUsers
            ,Subscription subscription)
        {
            if(organizationUsers.Count == 0 )
                throw new OrganizationUsersCollectionCannotBeEmptyException();

            Id = id;
            _name = name;
            _offices = offices;
            _organizationUsers = organizationUsers;
            _subscription = subscription;

        }
         
        public void AddUser(ApplicationUser user)
        {
            bool aleadyExists = _organizationUsers.Any(u => u.Id == user.Id);

            if (aleadyExists)
                throw new UserIsAlreadyMemberOfThisOrganizationException(user.Id);
            else if (!_isUnlimited && _usedSlots >= _userLimit)
                throw new OrganizationNotEnoughSlotsException();
            
            _organizationUsers.Add(user);
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
            bool alreadyExists = _organizationUsers.Any(u => u.Id == user.Id);

            
            if (alreadyExists && _organizationUsers.Count <= 1)
                throw new CantRemoveOnlyUserException(user);
            else if (alreadyExists)
                _organizationUsers.Remove(user);
            else
                throw new UserIsNotAMemberOfThisOrganization(user.Id);


        }
        public void RemoveRangeUsers(ICollection<ApplicationUser> users)
        {
            foreach (ApplicationUser user in users)
            {
                RemoveUser(user);
            }
        }
        internal bool IsUnlimited() => _isUnlimited;

    }
}
