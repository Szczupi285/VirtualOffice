using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.ValueObjects.Subscription;

namespace VirtualOffice.Domain.Entities
{
    public class Subscription
    {
        public SubscriptionId Id { get; private set; }

        internal SubscriptionStartDate _subStartDate;

        internal SubscriptionEndDate _subEndDate;

        public SubscriptionTypeEnum _subType { get; private set; }

        public SubscriptionFee _subscriptionFee { get; private set; } // must be decimal due to taxes

        public bool _isPayed { get; private set; } = false;


        internal Subscription(SubscriptionId id, SubscriptionStartDate startDate, SubscriptionTypeEnum type, decimal subscriptionFee, bool isPayed)
        {
            Id = id;
            _subStartDate = startDate;
            _subEndDate = startDate.Value.AddDays(30);
            _subType = type;
            _subscriptionFee = subscriptionFee;
            _isPayed = isPayed;
        }

        public void UpdateSubType(SubscriptionTypeEnum subType) => _subType = subType;

        public void Pay() => _isPayed = true;
    }
}
