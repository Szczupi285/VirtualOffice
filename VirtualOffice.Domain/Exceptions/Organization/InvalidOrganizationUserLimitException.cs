using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class InvalidOrganizationUserLimitException : VirtualOfficeException
    {
        public ushort? _value;

        public InvalidOrganizationUserLimitException(ushort? value) : base($"Value: '{value}' is not in the range 1-1000")
        {
            _value = value;
        }
    }
}