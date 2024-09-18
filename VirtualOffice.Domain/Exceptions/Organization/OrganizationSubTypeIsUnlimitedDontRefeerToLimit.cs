using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class OrganizationSubTypeIsUnlimitedDontRefeerToLimit : VirtualOfficeException
    {
        public OrganizationSubTypeIsUnlimitedDontRefeerToLimit() : base("You can't refeer to UserLimit while subscription type is unlimited")
        {
        }
    }
}