using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Office
{
    public class OfficeDescriptionIsNullException : VirtualOfficeException
    {
        public OfficeDescriptionIsNullException() : base("Office description is null")
        {
        }
    }
}