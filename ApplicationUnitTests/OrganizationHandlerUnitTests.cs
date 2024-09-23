﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.Handlers.OrganizationHandlers;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Application.Exceptions.Organization;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace ApplicationUnitTests
{
    public class OrganizationHandlerUntTests
    {
        private readonly Guid OrgGuid = Guid.NewGuid();
        private readonly Guid OfficeGuid = Guid.NewGuid();
        private readonly Organization _organization;
        private readonly AddOfficeHandler _addOffHand;
        private readonly AddOrganizationOfficeUsersHandler _addOrgOffUsrHand;
        private readonly AddOrganizationUsersHandler _addOrgUsrHand;
        private readonly CreateOrganizationHandler _creOrgHand;
        private readonly DeleteOfficeHandler _delOffHand;
        private readonly DeleteOrganizationHandler _delOrgHand;
        private readonly DeleteOrganizationOfficeUsersHandler _delOrgOffUsrHand;
        private readonly DeleteOrganizationUsersHandler _delOrgUsrHand;
        private readonly UpdateOrganizationNameHandler _updOrgNameHand;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly Mock<IOrganizationRepository> _repositoryMock;
        private readonly Mock<IOrganizationReadService> _readServiceMock;

        public OrganizationHandlerUntTests()
        {
            _repositoryMock = new Mock<IOrganizationRepository>();
            _readServiceMock = new Mock<IOrganizationReadService>();
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _user2 = new ApplicationUser(Guid.NewGuid(), "Jane", "Roe");

            Office office = new Office(OfficeGuid, "Name", "Desc", new HashSet<ApplicationUser>());
            _organization = new Organization(OrgGuid, "Name", new HashSet<Office>(),
                new HashSet<ApplicationUser>() { _user1, _user2 }, new Subscription(Guid.NewGuid(), DateTime.UtcNow.AddDays(1), SubscriptionTypeEnum.Trial, true));
            _organization.AddOffice(office);

            _addOffHand = new AddOfficeHandler(_repositoryMock.Object, _readServiceMock.Object);
            _addOrgOffUsrHand = new AddOrganizationOfficeUsersHandler(_repositoryMock.Object, _readServiceMock.Object);
            _addOrgUsrHand = new AddOrganizationUsersHandler(_repositoryMock.Object, _readServiceMock.Object);
            _creOrgHand = new CreateOrganizationHandler(_repositoryMock.Object);
            _delOffHand = new DeleteOfficeHandler(_repositoryMock.Object, _readServiceMock.Object);
            _delOrgHand = new DeleteOrganizationHandler(_repositoryMock.Object, _readServiceMock.Object);
            _delOrgOffUsrHand = new DeleteOrganizationOfficeUsersHandler(_repositoryMock.Object, _readServiceMock.Object);
            _delOrgUsrHand = new DeleteOrganizationUsersHandler(_repositoryMock.Object, _readServiceMock.Object);
            _updOrgNameHand = new UpdateOrganizationNameHandler(_repositoryMock.Object, _readServiceMock.Object);
        }

        [Fact]
        public async Task AddOfficeHandler_ShouldThrowOrganizationDoesNotExistsException()
        {
            // Arrange
            var request = new AddOffice(Guid.NewGuid(), "Name", "Description", new HashSet<ApplicationUser>() { _user1 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<OrganizationDoesNotExistsException>(() => _addOffHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task AddOfficeHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new AddOffice(OrgGuid, "Name", "Description", new HashSet<ApplicationUser>() { _user1 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.OrganizationId)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.OrganizationId)).ReturnsAsync(_organization);
            // Act
            await _addOffHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_organization), Times.Once);
        }

        [Fact]
        public async Task AddOrganizationOfficeUserHandler_ShouldThrowOrganizationDoesNotExistsException()
        {
            // Arrange
            var request = new AddOrganizationOfficeUsers(Guid.NewGuid(), Guid.NewGuid(), new HashSet<ApplicationUser>() { _user1 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<OrganizationDoesNotExistsException>(() => _addOrgOffUsrHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task AddOrganizationOfficeUserHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new AddOrganizationOfficeUsers(OrgGuid, OfficeGuid, new HashSet<ApplicationUser>() { _user1 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.OrganizationId)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.OrganizationId)).ReturnsAsync(_organization);
            // Act
            await _addOrgOffUsrHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_organization), Times.Once);
        }

        [Fact]
        public async Task AddOrganizationUsersHandler_ShouldThrowOrganizationDoesNotExistsException()
        {
            // Arrange
            var request = new AddOrganizationUsers(Guid.NewGuid(), new HashSet<ApplicationUser>() { _user1 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<OrganizationDoesNotExistsException>(() => _addOrgUsrHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task AddOrganizationUsersHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new AddOrganizationUsers(OrgGuid, new HashSet<ApplicationUser>() { _user1 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync(_organization);
            // Act
            await _addOrgUsrHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_organization), Times.Once);
        }

        [Fact]
        public async Task CreateOrganizationHandler_ShouldCallAddOnce()
        {
            // Arrange
            var request = new CreateOrganization("Name", new Subscription(Guid.NewGuid(), DateTime.UtcNow.AddDays(1), SubscriptionTypeEnum.Trial, true), _user1);
            // Act
            await _creOrgHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Organization>()), Times.Once);
        }

        [Fact]
        public async Task DeleteOfficeHandler_ShouldThrowOrganizationDoesNotExistsException()
        {
            // Arrange
            var request = new DeleteOffice(Guid.NewGuid(), Guid.NewGuid());
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<OrganizationDoesNotExistsException>(() => _delOffHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteOfficeHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new DeleteOffice(OrgGuid, OfficeGuid);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.OrganizationId)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.OrganizationId)).ReturnsAsync(_organization);
            // Act
            await _delOffHand.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(x => x.UpdateAsync(_organization), Times.Once);
        }

        [Fact]
        public async Task DeleteOrganizationHandler_ShouldThrowOrganizationDoesNotExistsException()
        {
            // Arrange
            var request = new DeleteOrganization(Guid.NewGuid());
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<OrganizationDoesNotExistsException>(() => _delOrgHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteOrganizationHandler_ShouldCallDeleteOnce()
        {
            // Arrange
            var request = new DeleteOrganization(OrgGuid);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync(_organization);
            // Act
            await _delOrgHand.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(_organization), Times.Once);
        }

        [Fact]
        public async Task DeleteOrganizationOfficeUsersHandler_ShouldThrowOrganizationDoesNotExistsException()
        {
            // Arrange
            var request = new DeleteOrganizationOfficeUsers(Guid.NewGuid(), Guid.NewGuid(), new HashSet<ApplicationUser>() { _user1 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<OrganizationDoesNotExistsException>(() => _delOrgOffUsrHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteOrganizationOfficeUsersHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new DeleteOrganizationOfficeUsers(OrgGuid, OfficeGuid, new HashSet<ApplicationUser>() { _user1 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.OrganizationId)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.OrganizationId)).ReturnsAsync(_organization);

            // Act
            await _delOrgOffUsrHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_organization), Times.Once);
        }

        [Fact]
        public async Task DeleteOrganizationUsersHandler_ShouldThrowOrganizationDoesNotExistsException()
        {
            // Arrange
            var request = new DeleteOrganizationUsers(Guid.NewGuid(), new HashSet<ApplicationUser>() { _user1 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<OrganizationDoesNotExistsException>(() => _delOrgUsrHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteOrganizationUsersHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new DeleteOrganizationUsers(OrgGuid, new HashSet<ApplicationUser>() { _user1 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync(_organization);

            // Act
            await _delOrgUsrHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_organization), Times.Once);
        }

        [Fact]
        public async Task UpdateOrganizationNameHandler_ShouldThrowOrganizationDoesNotExistsException()
        {
            // Arrange
            var request = new UpdateOrganizationName(Guid.NewGuid(), "NewName");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.OrganizationId)).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<OrganizationDoesNotExistsException>(() => _updOrgNameHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateOrganizationNameHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new UpdateOrganizationName(OrgGuid, "NewName");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.OrganizationId)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.OrganizationId)).ReturnsAsync(_organization);

            // Act
            await _updOrgNameHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_organization), Times.Once);
        }
    }
}