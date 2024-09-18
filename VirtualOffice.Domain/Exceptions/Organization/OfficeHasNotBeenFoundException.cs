using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class OfficeHasNotBeenFoundException : VirtualOfficeException
    {
        private Guid Value;

        public OfficeHasNotBeenFoundException(Guid value) : base($"Office with id: {value} has not been found")
        {
            Value = value;
        }
    }
}