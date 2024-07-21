using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers;
using VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace ApplicationUnitTests
{
    public class EmployeeTaskHandlersUnitTest
    {
        private readonly Mock<IEmployeeTaskRepository> _repositoryMock;
        private readonly Mock<IEmployeeTaskReadService> _readServiceMock;
        private readonly AddAssignedEmployeesToEmployeeTaskHandler _AddAssgEmpHandler;
        private readonly RemoveAssignedEmployeesFromEmployeeTaskHandler _RemAssgEmpHandler;
        private readonly CreateEmployeeTaskHandler _CreEmpTaskHandler;
        private readonly DeleteEmployeeTaskHandler _DelEmpTaskHandler;
        private readonly UpdateEmployeeTaskHandler _UpdEmpTaskHandler;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;

        public EmployeeTaskHandlersUnitTest()
        {
            _repositoryMock = new Mock<IEmployeeTaskRepository>();
            _readServiceMock = new Mock<IEmployeeTaskReadService>();
            _AddAssgEmpHandler = new AddAssignedEmployeesToEmployeeTaskHandler(_repositoryMock.Object, _readServiceMock.Object);
            _RemAssgEmpHandler = new RemoveAssignedEmployeesFromEmployeeTaskHandler(_repositoryMock.Object, _readServiceMock.Object);
            _CreEmpTaskHandler = new CreateEmployeeTaskHandler(_repositoryMock.Object);
            _DelEmpTaskHandler = new DeleteEmployeeTaskHandler(_repositoryMock.Object, _readServiceMock.Object);
            _UpdEmpTaskHandler = new UpdateEmployeeTaskHandler(_repositoryMock.Object, _readServiceMock.Object);
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _user2 = new ApplicationUser(Guid.NewGuid(), "Jane", "Roe");
        }
    }
}
