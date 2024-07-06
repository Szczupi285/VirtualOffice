using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    public class UserIsAlreadyMemberOfThisCollectionException : VirtualOfficeException
    {
        ApplicationUserId Id;
        public UserIsAlreadyMemberOfThisCollectionException(ApplicationUserId id) : base($"User with Id: {id} is already assigned to this Collection")
        {
            Id = id;
        }
    }
}
