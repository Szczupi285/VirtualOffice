using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.ValueObjects.Subscription;

namespace VirtualOffice.Domain.Entities
{
    public class Subscription
    {
        public SubscriptionId Id { get; private set; }

        private SubscriptionStartDate _subStartDate;

        private SubscriptionEndDate _subEndDate;

        public SubscriptionTypeEnum _subType { get; private set; }

        internal Subscription(SubscriptionId id, SubscriptionStartDate startDate, 
            SubscriptionEndDate endDate, SubscriptionTypeEnum type)
        {
            Id = id;
            _subStartDate = startDate;
            _subEndDate = endDate;
            _subType = type;
        }
    }
}
