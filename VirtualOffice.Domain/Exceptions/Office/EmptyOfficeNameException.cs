using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Office
{
    public class EmptyOfficeNameException : VirtualOfficeException
    {
        public EmptyOfficeNameException() : base("Office name cannot be empty")
        {
        }
    }
}