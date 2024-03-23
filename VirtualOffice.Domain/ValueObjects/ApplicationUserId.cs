using VirtualOffice.Domain.Exceptions;

namespace VirtualOffice.Domain.ValueObjects
{
    public class ApplicationUserId
    {
        public Guid Value { get; }

        public ApplicationUserId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyApplicationUserNameException();
            }

            Value = value;
        }

        public static implicit operator Guid(ApplicationUserId id)
            => id.Value;

        public static implicit operator ApplicationUserId(Guid id)
            => new(id);
    }
}
