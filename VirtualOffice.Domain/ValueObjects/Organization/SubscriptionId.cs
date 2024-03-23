using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.Organization
{
    public record SubscriptionId
    {
        public Guid Value { get; }

        public SubscriptionId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new NotImplementedException();
            }

            Value = value;
        }

        public static implicit operator Guid(SubscriptionId id)
            => id.Value;

        public static implicit operator SubscriptionId(Guid id)
            => new(id);
    }
}
