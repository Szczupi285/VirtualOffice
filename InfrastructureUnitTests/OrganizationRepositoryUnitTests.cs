using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Infrastructure.EF.Repositories;
using VirtualOffice.Infrastructure.EF;
using VirtualOffice.Domain.Consts;

namespace InfrastructureUnitTests
{
    public class OrganizationRepositoryUnitTests
    {
        private readonly WriteDbContext _dbContext;
        private readonly OrganizationRepository _repository;
        private readonly Guid _offGuid1;
        private readonly Guid _offGuid2;
        private readonly Guid _offGuid3;
        private readonly Guid _orgGuid1;
        private readonly Guid _orgGuid2;
        private readonly Guid _orgGuid3;
        private readonly Guid _guid1;
        private readonly Guid _guid2;
        private readonly Guid _guid3;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly ApplicationUser _user3;
        private readonly List<Organization> _data;
        private readonly HashSet<Office> _offices;
        private readonly HashSet<ApplicationUser> _users;

        public OrganizationRepositoryUnitTests()
        {
            _orgGuid1 = Guid.NewGuid();
            _orgGuid2 = Guid.NewGuid();
            _orgGuid3 = Guid.NewGuid();

            _offGuid1 = Guid.NewGuid();
            _offGuid2 = Guid.NewGuid();
            _offGuid3 = Guid.NewGuid();

            _guid1 = Guid.NewGuid();
            _guid2 = Guid.NewGuid();
            _guid3 = Guid.NewGuid();

            _user1 = new(_guid1, "NameOne", "SurnameOne");
            _user2 = new(_guid2, "NameTwo", "SurnameTwo");
            _user3 = new(_guid3, "NameThree", "SurnameThree");

            _users = new HashSet<ApplicationUser> { _user1, _user2 };

            _offices = new HashSet<Office>()
            {
                new Office(_offGuid1, "name", "desc", _users),
                new Office(_offGuid2, "name", "desc", new HashSet<ApplicationUser> {_user1}),
                new Office(_offGuid3, "name", "desc", new HashSet<ApplicationUser> {_user2})
            };

            _data = new List<Organization>
            {
                new(_orgGuid1, "HR", _offices,
                _users, new Subscription(Guid.NewGuid(), DateTime.UtcNow, SubscriptionTypeEnum.Trial, true)),
            };

            // setup in memory db
            var options = new DbContextOptionsBuilder<WriteDbContext>()
             .UseInMemoryDatabase(databaseName: "Test")
             .Options;

            _dbContext = new WriteDbContext(options);
            _repository = new OrganizationRepository(_dbContext);
            _dbContext.Organizations.AddRange(_data[0]);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_orgGuid1);
            // Assert
            Assert.Equal(_data[0], result);
        }

        [Fact]
        public async Task GetByIdAsync_OrganizationsUsersShouldEqual()
        {
            // Act
            var reasult = await _repository.GetByIdAsync(_orgGuid1);

            // Assert
            Assert.Equal(_data[0]._organizationUsers, _users);
        }

        // this unit test purpose is checking if GetByIdAsync properly include office and officeMembers
        [Fact]
        public async Task GetByIdAsync_OfficeUsersShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_orgGuid1);
            var office = result.GetOfficeById(_offGuid1);
            // Assert
            Assert.Equal(office._members, _users);
        }

        [Fact]
        public async Task GetByIdAsync_OfficeUsersShouldEqual_Case2()
        {
            // Act
            var result = await _repository.GetByIdAsync(_orgGuid1);
            var office = result.GetOfficeById(_offGuid2);
            // Assert
            Assert.Equal(office._members, new HashSet<ApplicationUser> { _user1 });
        }

        [Fact]
        public async Task AddAsync_NewOrganizationAdded_ShouldContain()
        {
            // Arrange
            var tempGuid = Guid.NewGuid();
            Organization org = new(tempGuid, "HR", new HashSet<Office>(),
                new HashSet<ApplicationUser>() { _user1 }, new Subscription(Guid.NewGuid(), DateTime.UtcNow, SubscriptionTypeEnum.Trial, true));
            // Act
            await _repository.AddAsync(org);
            // Assert
            Assert.True(_dbContext.Organizations.Contains(org));
        }

        [Fact]
        public async Task AddAsync_OrganizationDeleted_ShouldNotContain()
        {
            // Act
            Assert.True(_dbContext.Organizations.Contains(_data[0]));
            await _repository.DeleteAsync(_data[0]);
            // Assert
            Assert.False(_dbContext.Organizations.Contains(_data[0]));
        }

        [Fact]
        public async Task UpdateAsync_OrganizationUsersUpdated_ShouldContainAddedUser()
        {
            // Arrange
            Guid tempGuid = Guid.NewGuid();
            ApplicationUser newUser = new(tempGuid, "NameOne", "SurnameOne");

            var org = await _repository.GetByIdAsync(_orgGuid1);
            // Act
            org.AddUser(newUser);
            await _repository.UpdateAsync(org);

            // Assert
            bool contain = _dbContext.Organizations.First(x => x.Id == org.Id)
                ._organizationUsers.Contains(newUser);
            Assert.True(contain);
        }

        [Fact]
        public async Task UpdateAsync_OfficesUpdated_ShouldContainAddedUser()
        {
            // Arrange
            Guid tempGuid = Guid.NewGuid();
            Office newOffice = new(tempGuid, "name", "desc", _users);

            var org = await _repository.GetByIdAsync(_orgGuid1);
            // Act
            org.AddOffice(newOffice);
            await _repository.UpdateAsync(org);

            // Assert
            bool contain = _dbContext.Organizations.First(x => x.Id == org.Id)
                ._offices.Contains(newOffice);
            Assert.True(contain);
        }
    }
}