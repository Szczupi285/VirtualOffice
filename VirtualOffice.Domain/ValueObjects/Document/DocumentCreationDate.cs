using VirtualOffice.Domain.Exceptions.Document;

namespace VirtualOffice.Domain.ValueObjects.Document
{
    public sealed record DocumentCreationDate
    {
        public DateTime Value { get; }

        public DocumentCreationDate(DateTime value)
        {
            if (value > DateTime.UtcNow.AddMinutes(-1) && value < DateTime.UtcNow.AddMinutes(1))
                Value = value;
            else
                throw new DocumentCreationDateCannotBeEitherPastOrFutureException(value);
        }

        public static implicit operator DateTime(DocumentCreationDate startDate)
            => startDate.Value;

        public static implicit operator DocumentCreationDate(DateTime startDate)
            => new(startDate);
    }
}