using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.ApplicationUser
{
    public sealed record ApplicationUserId : AbstractRecordId
    {
        public ApplicationUserId(Guid value) : base(value, new EmptyApplicationUserIdException())
        {
        }

        public static implicit operator ApplicationUserId(Guid id)
            => new(id);
    }
}
