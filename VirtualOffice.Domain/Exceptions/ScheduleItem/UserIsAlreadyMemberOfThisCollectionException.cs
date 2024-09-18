using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    public class UserIsAlreadyMemberOfThisCollectionException : VirtualOfficeException
    {
        private ApplicationUserId Id;

        public UserIsAlreadyMemberOfThisCollectionException(ApplicationUserId id) : base($"User with Id: {id} is already assigned to this Collection")
        {
            Id = id;
        }
    }
}