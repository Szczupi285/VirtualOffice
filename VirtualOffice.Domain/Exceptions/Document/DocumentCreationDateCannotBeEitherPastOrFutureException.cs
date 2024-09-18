using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class DocumentCreationDateCannotBeEitherPastOrFutureException : VirtualOfficeException
    {
        private DateTime Value;

        public DocumentCreationDateCannotBeEitherPastOrFutureException(DateTime value) : base($"DocumentCreationDate: {value} cannot be either in the past or in the future")
        {
            Value = value;
        }
    }
}