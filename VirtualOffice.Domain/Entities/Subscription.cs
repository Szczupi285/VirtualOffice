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

        public SubscriptionFee _subscriptionFee // must be decimal due to taxes
        { 
            get
            {
                switch (_subType)
                {
                    case SubscriptionTypeEnum.Basic:
                        return new SubscriptionFee(100);
                    case SubscriptionTypeEnum.Enterprise:
                        return new SubscriptionFee(200);
                    case SubscriptionTypeEnum.Premium:
                        return new SubscriptionFee(350);
                    case SubscriptionTypeEnum.Unlimited:
                        return new SubscriptionFee(600);
                    default:
                        return new SubscriptionFee(0);
                }
            }
            private set { }
        } 

        public bool _isPayed { get; private set; } = false;


        internal Subscription(SubscriptionId id, SubscriptionStartDate startDate, SubscriptionTypeEnum type, bool isPayed)
        {
            Id = id;
            _subStartDate = startDate;
            _subEndDate = startDate.Value.AddDays(30);
            _subType = type;
            _isPayed = isPayed;
        }

        public void UpdateSubType(SubscriptionTypeEnum subType) => _subType = subType;

        public void Pay() => _isPayed = true;
    }
}
