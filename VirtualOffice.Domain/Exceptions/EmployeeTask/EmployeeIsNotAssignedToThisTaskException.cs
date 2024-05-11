using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.EmployeeTask
{
    internal class UserIsNotAssignedToThisTaskException : VirtualOfficeException
    {
        ApplicationUserId Id;
        public UserIsNotAssignedToThisTaskException(ApplicationUserId id) : base($"User with Id: {id} is not assigned to this task")
        {
            Id = id;
        }
    }
}
