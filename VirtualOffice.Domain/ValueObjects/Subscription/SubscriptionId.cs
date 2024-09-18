using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Subscription;

namespace VirtualOffice.Domain.ValueObjects.Subscription
{
    public sealed record SubscriptionId : AbstractRecordId
    {
        public SubscriptionId(Guid value) : base(value, new EmptySubscriptionIdException())
        {
        }

        public static implicit operator SubscriptionId(Guid id)
            => new(id);
    }
}