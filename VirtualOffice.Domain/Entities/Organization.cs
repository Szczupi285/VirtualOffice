using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.DomainEvents;
using VirtualOffice.Domain.DomainEvents.OrganizationEvents;
using VirtualOffice.Domain.Exceptions.Organization;
using VirtualOffice.Domain.ValueObjects.Office;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.Entities
{

    public class Organization : AggregateRoot<OrganizationId>
    {
        public OrganizationName _name { get; private set; }

        public OrganizationUserLimit _userLimit {
            get
            {
                if(_subscription._subType == Consts.SubscriptionTypeEnum.Unlimited)
                {
                    return null!;
                }
                return (ushort)_subscription._subType;
            }
        }  

        public OrganizationUsedSlots _usedSlots { get => (ushort)_organizationUsers.Count(); } 

        public ushort? _slotsLeft
        {
            get
            { 
                if (_userLimit is null)
                    return null;
                else
                {
                    var slotsLeft = _userLimit - _usedSlots;
                    if (slotsLeft <= 0)
                        throw new OrganizationNotEnoughSlotsException();
                    else
                       return (ushort)slotsLeft;
                }
            }
        }

        public HashSet<Office> _offices { get; private set; }

        // consider this relation since office already contains 
        // users list, but we would have to check distinct users every time
        // we add user to office. Since one user may be a member of many offices
        public HashSet<ApplicationUser> _organizationUsers { get; private set; }

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

        public Organization(OrganizationId id, OrganizationName name,
             HashSet<Office> offices, HashSet<ApplicationUser> organizationUsers
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

        public Office GetOfficeById(OfficeId id) 
        {
            var office = _offices.FirstOrDefault(x => x.Id == id);
            if(office is null)
                throw new OfficeHasNotBeenFoundException(id);

            return office;
        }


        public void AddOffice(Office office)
        {
            if (office is null) 
                throw new ArgumentNullException("Office cannot be null");
            bool hasBeenAdded = _offices.Add(office);

            if (hasBeenAdded)
                AddEvent(new OfficeAdded(this, office));
        }
        public void RemoveOffice(Office office)
        {
            if(office is null)
                throw new ArgumentNullException("Office Cannot be null");
            else if (!_offices.Contains(office))
                throw new OfficeHasNotBeenFoundException(office.Id);

            _offices.Remove(office);
            AddEvent(new OfficeRemoved(this, office));
        }
        public void SetName(string name)
        {
            _name = name;
            AddEvent(new OrganizationNameSetted(this, _name));
        }

        public void AddUser(ApplicationUser user)
        {
            if (!_isUnlimited && _slotsLeft <= 0)
                throw new OrganizationNotEnoughSlotsException();

            bool HasBeenAdded = _organizationUsers.Add(user);

            if(HasBeenAdded)
                AddEvent(new UserAddedToOrganization(this, user));
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
            {
                _organizationUsers.Remove(user);
                AddEvent(new UserRemovedFromOrganization(this, user));
            }
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

        public void AddOfficeUser(ApplicationUser user, Office office)
        {
            if (!_offices.Contains(office))
                throw new OfficeHasNotBeenFoundException(office.Id);
            else if (!_organizationUsers.Contains(user))
                throw new UserIsNotAMemberOfThisOrganization(user.Id);
            else if(office.AddMember(user))
                AddEvent(new UserAddedToOffice(this, office, user));

        }
        public void AddRangeOfficeUsers(ICollection<ApplicationUser> users, Office office)
        {
            foreach(ApplicationUser user in users)
                AddOfficeUser(user, office);
        }

        public void RemoveOfficeUser(ApplicationUser user, Office office)
        {

            if (!_offices.Contains(office))
                throw new OfficeHasNotBeenFoundException(office.Id);
            else if (!_organizationUsers.Contains(user))
                throw new UserIsNotAMemberOfThisOrganization(user.Id);

            if (office.RemoveMember(user))
                AddEvent(new UserRemovedFromOffice(this, office, user));

        }

        public void RemoveRangeOfficeUsers(ICollection<ApplicationUser> users, Office office)
        {
            foreach (ApplicationUser user in users)
                RemoveOfficeUser(user, office);
        }

    }
}
