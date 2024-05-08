using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.EmployeeTask
{
    public class UserIsAlreadyAssignedToThisTaskException : VirtualOfficeException
    {
        ApplicationUserId Id;
        public UserIsAlreadyAssignedToThisTaskException(ApplicationUserId id) : base($"User with Id: {id} is already assigned to this task")
        {
            Id = id;
        }
    }
}
