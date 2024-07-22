using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.Handlers.OrganizationHandlers;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace ApplicationUnitTests
{
    public class OrganizationHandlerUntTests
    {
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

            _organization = new Organization(Guid.NewGuid(), "Name", new HashSet<Office>(),
                new HashSet<ApplicationUser>() { _user1, _user2 }, new Subscription(Guid.NewGuid(), DateTime.UtcNow.AddDays(1), SubscriptionTypeEnum.Trial, true));
          
            _addOffHand = new AddOfficeHandler(_repositoryMock.Object, _readServiceMock.Object);
            _addOrgOffUsrHand = new AddOrganizationOfficeUsersHandler(_repositoryMock.Object, _readServiceMock.Object);
            _addOrgUsrHand = new AddOrganizationUsersHandler(_repositoryMock.Object, _readServiceMock.Object);
            _creOrgHand = new CreateOrganizationHandler(_repositoryMock.Object);
            _delOffHand = new DeleteOfficeHandler(_repositoryMock.Object, _readServiceMock.Object);
            _delOrgHand = new DeleteOrganizationHandler(_repositoryMock.Object, _readServiceMock.Object);
            _delOrgOffUsrHand = new DeleteOrganizationOfficeUsersHandler(_repositoryMock.Object, _readServiceMock.Object);
            _delOrgUsrHand = new DeleteOrganizationUsersHandler(_repositoryMock.Object, _readServiceMock.Object);
            _updOrgNameHand = new UpdateOrganizationNameHandler(_repositoryMock.Object, _readServiceMock.Object);
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _user2 = new ApplicationUser(Guid.NewGuid(), "Jane", "Roe");
        }
    }
}
