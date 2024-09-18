using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class UserIsNotAMemberOfThisOrganization : VirtualOfficeException
    {
        private Guid Value;

        public UserIsNotAMemberOfThisOrganization(Guid value) : base($"User with Id: {value} is not a member of this organization")
        {
            Value = value;
        }
    }
}