using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.Entities
{
    public class Subscription
    {
        public SubscriptionId Id { get; private set; }

        // private OrganizationSubscriptionStartDate _subStartDate;

        // private OrganizationSubscriptionEndDate _subEndDate;

        // private OrganizationSubscriptionType _licenseType

        internal Subscription()
        {

        }
    }
}
