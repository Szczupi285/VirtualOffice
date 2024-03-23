namespace VirtualOffice.Shared.Abstractions.Exceptions
{
    public abstract class VirtualOfficeException : Exception
    {
        protected VirtualOfficeException(string? message) : base(message)
        {
        }
    }
}
