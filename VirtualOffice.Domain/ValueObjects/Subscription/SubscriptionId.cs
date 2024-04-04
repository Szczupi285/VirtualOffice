using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.Exceptions.Subscription;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

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
