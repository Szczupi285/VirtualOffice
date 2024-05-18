using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    internal class UserIsNotAssignedToThisScheduleItemException : VirtualOfficeException
    {
        ApplicationUserId Id;
        public UserIsNotAssignedToThisScheduleItemException(ApplicationUserId id) : base($"User with Id: {id} is not assigned to this ScheduleItem")
        {
            Id = id;
        }
    }
}
