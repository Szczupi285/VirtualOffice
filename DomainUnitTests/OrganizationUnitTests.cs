using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.DomainEvents.OrganizationEvents;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Office;
using VirtualOffice.Domain.Exceptions.Organization;
using VirtualOffice.Domain.ValueObjects.Office;
using VirtualOffice.Domain.ValueObjects.Organization;
using VirtualOffice.Domain.ValueObjects.Subscription;

namespace DomainUnitTests
{
    public class OrganizationUnitTests
    {
        private Organization _Org { get; set; }
        private ApplicationUser User { get; set; }
        private ApplicationUser UserNotAdded { get; set; }
        private Office Office { get; set; }
        private Office OfficeNotAdded { get; set; }
        public OrganizationUnitTests()
        {
            ApplicationUser user = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
            ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
            User = user;
            UserNotAdded = new ApplicationUser(Guid.NewGuid(), "NameOne", "SurnameOne");
            Office = new Office(Guid.NewGuid(), "OfficeName", "Description", new HashSet<ApplicationUser> { user });
            OfficeNotAdded = new Office(Guid.NewGuid(), "OfficeName", "Description", new HashSet<ApplicationUser> { user });
            Subscription subscription = new Subscription(Guid.NewGuid(), DateTime.UtcNow,SubscriptionTypeEnum.Trial, true);
            _Org = new Organization(Guid.NewGuid(), "Name", new HashSet<Office> { Office },
                new HashSet<ApplicationUser>(){ user, user1 }, subscription);
        }

        #region Events

        [Fact]
        public void AddOffice_ShouldNotRaiseOfficeAdded()
        {
            _Org.AddOffice(Office);
            var Event = _Org.Events.OfType<OfficeAdded>().SingleOrDefault();
            Assert.Null(Event);
        }
        [Fact]
        public void AddOffice_ShouldRaiseOfficeAdded()
        {
            _Org.AddOffice(OfficeNotAdded);
            var Event = _Org.Events.OfType<OfficeAdded>().Single();
        }
        [Fact]
        public void AddOffice_ShouldRaiseOfficeAdded_OrganizationShouldEqual()
        {
            _Org.AddOffice(OfficeNotAdded);
            var Event = _Org.Events.OfType<OfficeAdded>().Single();
            Assert.Equal(_Org, Event.organization);
        }
        [Fact]
        public void AddOffice_ShouldRaiseOfficeAdded_OfficeShouldEqual()
        {
            _Org.AddOffice(OfficeNotAdded);
            var Event = _Org.Events.OfType<OfficeAdded>().Single();
            Assert.Equal(OfficeNotAdded, Event.office);
        }
        [Fact]
        public void RemoveOffice_ShouldRaiseOfficeRemoved()
        {
            _Org.RemoveOffice(Office);
            var Event = _Org.Events.OfType<OfficeRemoved>().Single();
        }
        [Fact]
        public void RemoveOffice_ShouldRaiseOfficeRemoved_OrganizationShouldEqual()
        {
            _Org.RemoveOffice(Office);
            var Event = _Org.Events.OfType<OfficeRemoved>().Single();
            Assert.Equal(_Org, Event.organization);
        }
        [Fact]
        public void RemoveOffice_ShouldRaiseOfficeRemoved_OfficeShouldEqual()
        {
            _Org.RemoveOffice(Office);
            var Event = _Org.Events.OfType<OfficeRemoved>().Single();
            Assert.Equal(Office, Event.office);
        }
        [Fact]
        public void SetName_ShouldRaiseOrganizationNameSetted()
        {
            _Org.SetName("new");
            var Event = _Org.Events.OfType<OrganizationNameSetted>().Single();
        }
        [Fact]
        public void SetName_ShouldRaiseOrganizationNameSetted_OrganizationShouldEqual()
        {
            _Org.SetName("new");
            var Event = _Org.Events.OfType<OrganizationNameSetted>().Single();
            Assert.Equal(_Org, Event.organization);
        }
        [Fact]
        public void SetName_ShouldRaiseOrganizationNameSetted_NameShouldEqual()
        {
            _Org.SetName("new");
            var Event = _Org.Events.OfType<OrganizationNameSetted>().Single();
            Assert.Equal("new", Event.name);
        }
        [Fact]
        public void AddUser_ShouldNotRaiseUserAddedToOrganization()
        {
            _Org.AddUser(User);
            var Event = _Org.Events.OfType<UserAddedToOrganization>().SingleOrDefault();
            
            Assert.Null(Event);
        }
        [Fact]
        public void AddUser_ShouldRaiseUserAddedToOrganization()
        {
            _Org.AddUser(UserNotAdded);
            var Event = _Org.Events.OfType<UserAddedToOrganization>().SingleOrDefault();
        }
        [Fact]
        public void AddUser_ShouldRaiseUserAddedToOrganization_OrganizationShouldEqual()
        {
            _Org.AddUser(UserNotAdded);
            var Event = _Org.Events.OfType<UserAddedToOrganization>().Single();
            Assert.Equal(_Org, Event.organization);
        }
        [Fact]
        public void AddUser_ShouldRaiseUserAddedToOrganization_userShouldEqual()
        {
            _Org.AddUser(UserNotAdded);
            var Event = _Org.Events.OfType<UserAddedToOrganization>().Single();
            Assert.Equal(UserNotAdded, Event.user);
        }
        [Fact]
        public void RemoveUser_ShouldRaiseUserAddedToOrganization()
        {
            _Org.RemoveUser(User);
            var Event = _Org.Events.OfType<UserAddedToOrganization>().SingleOrDefault();
        }
        [Fact]
        public void RemoveUser_ShouldRaiseUserAddedToOrganization_OrganizationShouldEqual()
        {
            _Org.RemoveUser(User);
            var Event = _Org.Events.OfType<UserRemovedFromOrganization>().Single();
            Assert.Equal(_Org, Event.organization);
        }
        [Fact]
        public void RemoveUser_ShouldRaiseUserAddedToOrganization_userShouldEqual()
        {
            _Org.RemoveUser(User);
            var Event = _Org.Events.OfType<UserRemovedFromOrganization>().Single();
            Assert.Equal(User, Event.user);
        }
        #endregion

        #region OrganizationId
        [Fact]
        public void EmptyOrganizationId_ShouldReturnEmptyOrganizationIdException()
        {
            Assert.Throws<EmptyOrganizationIdException>(()
                => new OrganizationId(Guid.Empty));
        }

        [Fact]
        public void ValidOrganizationId_ValidGuidToOrganizationIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            OrganizationId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidOrganizationId_ValidOrganizationIdToGuidConversion_ShouldEqual()
        {

            OrganizationId id = new OrganizationId(Guid.NewGuid());

            Guid guid = id;

            Assert.Equal(id.Value, guid);
        }

        #endregion

        #region OrganizationName
        [Fact]
        public void EmptyOrganizationName_ShouldReturnEmptyOrganizationNameException()
        {
            Assert.Throws<EmptyOrganizationNameException>(()
                => new OrganizationName(String.Empty));
        }

        [Theory]
        [InlineData("Thisorganizationnameislongerthan100charactersThisorganizationnameislongerthan100charactersThisorganization")]
        public void InvalidOrganizationName_ShouldReturnInvalidOrganizationNameException(string input)
        {
            Assert.Throws<InvalidOrganizationNameException>(()
                => new OrganizationName(input));
        }

        [Theory]
        [InlineData("Thisorganizationnameisvalid")]
        [InlineData("Organization")]
        [InlineData("O")]
        public void ValidOrganizationName_StringShouldMatch(string input)
        {
            OfficeName value = input;
            Assert.Equal(input, value);
        }
        #endregion

        #region OrganizationUserLimit
        [Theory]
        [InlineData(0)]
        [InlineData(1001)]
        public void InvalidOrganizationUserLimit_ShouldReturnInvalidOrganizationUserLimitException(ushort input)
        {
            Assert.Throws<InvalidOrganizationUserLimitException>(()
                => new OrganizationUserLimit(input));
        }
        [Fact]
        public void ValidOrganizationUserLimitUnlimited_ShouldBeNull()
        {
            OrganizationUserLimit value = null!;
            Assert.Null(value);
        }
        [Fact]
        public void OrganiazationUserLimitGetSubTypeUnlimited()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { }, new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);

            Assert.Null(org._userLimit);
        }
        
        // since user with sub type none should't be able to use application at all we're throwing exception for userLimit 0 which is equal to no subscribtion
        [Fact]
        public void OrganiazationUserLimitGetSubTypeNone_ShouldThrowInvalidOrganizationUserLimitException()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { }, new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);


            Assert.Throws<InvalidOrganizationUserLimitException>(()
                => 0 == org._userLimit);
        }
        [Fact]
        public void OrganiazationUserLimitGetSubTypeTrial()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Trial, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { }, new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);

            Assert.True(3 == org._userLimit);
        }
        [Fact]
        public void OrganiazationUserLimitGetSubTypeBasic()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Basic, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { }, new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);

            Assert.True(30 == org._userLimit);
        }

        [Fact]
        public void OrganiazationUserLimitGetSubTypeEnterprise()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Enterprise, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { }, new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);

            Assert.True(100 == org._userLimit);
        }
        [Fact]
        public void OrganiazationUserLimitGetSubTypePremium()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Premium, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { }, new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);

            Assert.True(500 == org._userLimit);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(999)]
        public void ValidOrganizationUserLimit_NumberShouldMatch(ushort input)
        {
            OrganizationUserLimit value = (OrganizationUserLimit)input;
            Assert.Equal(input, value.Value);
        }
        #endregion

        #region OrganizationUsedSlots
        [Theory]
        [InlineData(0)]
        public void InvalidOrganizationUsedSlots_ShouldReturnInvalidOrganizationUsedSlotsException(ushort input)
        {
            Assert.Throws<InvalidOrganizationUsedSlotsException>(()
                => new OrganizationUsedSlots(input));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1000)]
        public void ValidOrganizationUsedSlots_NumberShouldMatch(ushort input)
        {
            OrganizationUserLimit value = (OrganizationUserLimit)input;
            Assert.Equal(input, value.Value);
        }
        #endregion

        #region _isUnlimited

        [Fact]
        public void OrganiazationIsUnlimited_SubTypeUnlimited_ShouldReturnTrue()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { }, new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);


            Assert.True(org.IsUnlimited());
        }
        [Fact]
        public void OrganiazationIsUnlimited_SubTypeNone_ShouldReturnFalse()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.None, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { }, new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);

            Assert.False(org.IsUnlimited());
        }
        [Fact]
        public void OrganiazationIsUnlimited_SubTypeTrial_ShouldReturnFalse()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Trial, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { }, new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);

            Assert.False(org.IsUnlimited());
        }

        [Fact]
        public void OrganiazationIsUnlimited_SubTypeBasic_ShouldReturnFalse()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Basic, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { }, new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);

            Assert.False(org.IsUnlimited());
        }
        [Fact]
        public void OrganiazationIsUnlimited_SubTypeEntreprise_ShouldReturnFalse()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Enterprise, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { }, new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);

            Assert.False(org.IsUnlimited());
        }
        [Fact]
        public void OrganiazationIsUnlimited_SubTypePremium_ShouldReturnFalse()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Premium, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { }, new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);

            Assert.False(org.IsUnlimited());
        }


        #endregion

        #region _usedSlots
        [Fact]
        public void AddUser_UsedSlotsShouldIncrement_ShouldReturnTrue()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();
            
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { },
            new HashSet<ApplicationUser> { new ApplicationUser(Guid.NewGuid(), "Name", "surname") }, subscription);

            uint usersPreAdd = org._usedSlots;
            org.AddUser(new ApplicationUser(Guid.NewGuid(), "Name", "surname"));

            Assert.True(usersPreAdd == org._usedSlots - 1);
        }

        [Fact]
        public void AddRangeUsers_UsedSlotsShouldIncrement_ShouldReturnTrue()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();

            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { },
            new HashSet<ApplicationUser> { new ApplicationUser(Guid.NewGuid(), "Name", "surname") }, subscription);

            uint usersPreAdd = org._usedSlots;
            HashSet<ApplicationUser> usersToAdd = new HashSet<ApplicationUser>()
            {
                new ApplicationUser(Guid.NewGuid(), "Name", "surname"),
                new ApplicationUser(Guid.NewGuid(), "Name", "surname"),
                new ApplicationUser(Guid.NewGuid(), "Name", "surname"),
                new ApplicationUser(Guid.NewGuid(), "Name", "surname"),
            };
            org.AddRangeUsers(usersToAdd);

            Assert.True(usersPreAdd == org._usedSlots - 4);
        }


        [Fact]
        public void RemoveUser_UsedSlotsShouldDecrement_ShouldReturnTrue()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { },
            new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname"),
            new ApplicationUser(Guid.NewGuid(), "Name", "surname") }, subscription);

            uint usersPreRem = org._usedSlots;
            org.RemoveUser(org._organizationUsers.First(u => u.Id.Equals(guid3)));

            Assert.True(usersPreRem == org._usedSlots + 1);
        }

        [Fact]
        public void RemoveRangeUsers_UsedSlotsShouldDecrement_ShouldReturnTrue()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();
            Guid guidu1 = Guid.NewGuid();
            Guid guidu2 = Guid.NewGuid();
            Guid guidu3 = Guid.NewGuid();
            Guid guidu4 = Guid.NewGuid();
            ApplicationUser user1 = new ApplicationUser(guidu1, "Name", "surname");
            ApplicationUser user2 = new ApplicationUser(guidu2, "Name", "surname");
            ApplicationUser user3 = new ApplicationUser(guidu3, "Name", "surname");
            ApplicationUser user4 = new ApplicationUser(guidu4, "Name", "surname");

            HashSet<ApplicationUser> usersToRemove = new HashSet<ApplicationUser>()
            {
                user1,
                user2, 
                user3,
                user4,
            };

            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { },
            new HashSet<ApplicationUser>()
            {
                new ApplicationUser(Guid.NewGuid(), "Name", "surname"),
                user1,
                user2,
                user3,
                user4,
            }, 
            subscription);

            uint usersPreRem = org._usedSlots;
            org.RemoveRangeUsers(usersToRemove);

            Assert.True(usersPreRem == org._usedSlots + 4);
        }


        #endregion

        #region Adding users
        [Fact]
        public void AddUser_ShouldContainUser()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();

            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { },
            new HashSet<ApplicationUser> { new ApplicationUser(Guid.NewGuid(), "Name", "surname") }, subscription);

            ApplicationUser user = new ApplicationUser(Guid.NewGuid(), "Mike", "Jackson");

            org.AddUser(user);

            Assert.Contains(user, org._organizationUsers);
        }
        
        [Fact]
        public void AddUser_WhenNotEnoughtSlots_ShouldThrowException()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Trial, true);
            Guid guid2 = Guid.NewGuid();

            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { },
            new HashSet<ApplicationUser> { new ApplicationUser(Guid.NewGuid(), "Name", "surname") }, subscription);

            ApplicationUser user = new ApplicationUser(Guid.NewGuid(), "Mike", "Jackson");
            ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "Mike", "Jackson");
            ApplicationUser user2 = new ApplicationUser(Guid.NewGuid(), "Mike", "Jackson");

            org.AddUser(user);
            org.AddUser(user1);

            Assert.Throws<OrganizationNotEnoughSlotsException>(() => org.AddUser(user2));
        }
        [Fact]
        public void AddRangeUsers_ShouldContainUsers()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();

            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { },
            new HashSet<ApplicationUser> { new ApplicationUser(Guid.NewGuid(), "Name", "surname") }, subscription);

            HashSet<ApplicationUser> usersToAdd = new HashSet<ApplicationUser>()
            {
                new ApplicationUser(Guid.NewGuid(), "Name", "surname"),
                new ApplicationUser(Guid.NewGuid(), "Name", "surname"),
                new ApplicationUser(Guid.NewGuid(), "Name", "surname"),
                new ApplicationUser(Guid.NewGuid(), "Name", "surname"),
            };

            org.AddRangeUsers(usersToAdd);

            foreach (var user in usersToAdd)
            {
                Assert.Contains(user, org._organizationUsers);
            }
        }
        #endregion

        #region Removing users

        [Fact]
        public void RemoveUser_ShouldNotContainUser()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            ApplicationUser user = new ApplicationUser(Guid.NewGuid(), "Mike", "Jackson");
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { },
            new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname"), user, }, subscription);

            org.RemoveUser(user);

            Assert.DoesNotContain(user, org._organizationUsers);
        }

        [Fact]
        public void RemoveUser_OnlyUser_ShouldReturnCantRemoveOnlyUserException()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            ApplicationUser user = new ApplicationUser(Guid.NewGuid(), "Mike", "Jackson");
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { },
            new HashSet<ApplicationUser> { user }, subscription);

            Assert.Throws<CantRemoveOnlyUserException>(() => org.RemoveUser(user));
        }

        [Fact]
        public void RemoveUser_NotAMember_ShouldReturnUserIsNotAMemberOfThisOrganization()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            ApplicationUser user = new ApplicationUser(Guid.NewGuid(), "Mike", "Jackson");
            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { },
            new HashSet<ApplicationUser> { new ApplicationUser(guid3, "Name", "surname") }, subscription);

            Assert.Throws<UserIsNotAMemberOfThisOrganization>(() => org.RemoveUser(user));
        }

        [Fact]
        public void RemoveRangeUsers_ShouldNotContainUsers()
        {
            Guid guid1 = Guid.NewGuid();
            Subscription subscription = new Subscription(guid1, DateTime.UtcNow, SubscriptionTypeEnum.Unlimited, true);
            Guid guid2 = Guid.NewGuid();

            Guid guidu1 = Guid.NewGuid();
            Guid guidu2 = Guid.NewGuid();
            Guid guidu3 = Guid.NewGuid();
            Guid guidu4 = Guid.NewGuid();

            ApplicationUser user1 = new ApplicationUser(guidu1, "Name", "surname");
            ApplicationUser user2 = new ApplicationUser(guidu2, "Name", "surname");
            ApplicationUser user3 = new ApplicationUser(guidu3, "Name", "surname");
            ApplicationUser user4 = new ApplicationUser(guidu4, "Name", "surname");

            HashSet<ApplicationUser> usersToRemove = new HashSet<ApplicationUser>()
            {
               user1,
               user2,
               user3,
               user4,
            };

            Organization org = new Organization(guid2, "Organization", new HashSet<Office> { },
            new HashSet<ApplicationUser> 
            { 
                new ApplicationUser(Guid.NewGuid(), "Name", "surname"),
                user1,
                user2,
                user3,
                user4,
            }, subscription);

          

            org.RemoveRangeUsers(usersToRemove);

            foreach (var user in usersToRemove)
            {
                Assert.DoesNotContain(user, org._organizationUsers);
            }
        }

        #endregion
    }
}
