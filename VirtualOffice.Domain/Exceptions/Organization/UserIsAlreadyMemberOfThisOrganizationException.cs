using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class UserIsAlreadyMemberOfThisOrganizationException : VirtualOfficeException
    {
        private Guid Value;

        public UserIsAlreadyMemberOfThisOrganizationException(Guid value) : base($"User with Id: {value} is already member of this organization")
        {
            Value = value;
        }
    }
}