using VirtualOffice.Domain.ValueObjects.Subscription;

namespace VirtualOffice.Domain.Entities
{
    public class Subscription
    {
        public SubscriptionId Id { get; private set; }

        // private SubscriptionStartDate _subStartDate;

        // private SubscriptionEndDate _subEndDate;

        // private SubscriptionType _licenseType

        internal Subscription()
        {

        }
    }
}
