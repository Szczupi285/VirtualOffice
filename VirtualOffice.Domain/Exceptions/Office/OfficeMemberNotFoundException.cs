using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Office
{
    public class OfficeMemberNotFoundException : VirtualOfficeException
    {
        private string Value;

        public OfficeMemberNotFoundException(string value) : base($"Cannot find office member with the provided input: {value}.")
        {
            Value = value;
        }
    }
}