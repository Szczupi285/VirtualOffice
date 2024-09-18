using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.SubscriptionService;
using VirtualOffice.Domain.ValueObjects.Subscription;

namespace VirtualOffice.Domain.Services
{
    public class SubscriptionService
    {
        //private  { get; set; }
        private ICollection<Subscription> _Subscriptions { get; set; }

        public int SubscriptionsCount => _Subscriptions.Count;

        public SubscriptionService(ICollection<Subscription> subscriptions)
        {
            _Subscriptions = subscriptions;
        }

        public void AddSubscription(Subscription subscription)
        {
            if (!DoesSubInThatPeriodAlreadyExists(subscription))
                _Subscriptions.Add(subscription);
            else
                throw new SubscriptionDatesOverlapException(subscription._subStartDate, subscription._subEndDate);
        }

        internal bool DoesSubInThatPeriodAlreadyExists(Subscription subscription)
        {
            if (_Subscriptions.Count == 0) return false;

            bool flag = true;
            foreach (Subscription sub in _Subscriptions)
            {
                if (subscription._subEndDate.Value < sub._subStartDate.Value ||
                    subscription._subStartDate.Value > sub._subEndDate.Value)
                    flag = false;
                else if (sub._subType == SubscriptionTypeEnum.None)
                    flag = false; // ignores subscriptions of type None as it is the default
                else
                    return true;
            }
            return flag;
        }

        public Subscription GetSubscriptionById(SubscriptionId id)
            => _Subscriptions.FirstOrDefault(s => s.Id == id) ?? throw new SubscriptionNotFoundException(id);

        public void AddSubscriptionRange(ICollection<Subscription> subscriptions)
        {
            foreach (Subscription sub in subscriptions)
            {
                AddSubscription(sub);
            }
        }

        private void UpgradeSubscriptionType(SubscriptionId id, SubscriptionTypeEnum subType)
        {
            Subscription subscription = _Subscriptions.FirstOrDefault(s => s.Id == id) ?? throw new SubscriptionNotFoundException(id);
            if (subscription._subType == SubscriptionTypeEnum.Unlimited || subscription._subType >= subType)
                throw new SubscriptionTypeCannotBeChangedToLowerTier(subscription._subType, subType);

            subscription.UpdateSubType(subType);
        }

        public void UpgradeSubscriptionTypeRange(ICollection<SubscriptionId> Ids, SubscriptionTypeEnum subscriptionType)
        {
            foreach (SubscriptionId id in Ids)
            {
                UpgradeSubscriptionType(id, subscriptionType);
            }
        }

        // consider what to return
        // return type none when higher tiers are not avalible
        public Subscription GetCurrentSubscription()
        {
            Subscription? currentSubscription = _Subscriptions.FirstOrDefault(s => DateTime.UtcNow > s._subStartDate.Value && DateTime.UtcNow < s._subEndDate.Value);
            if (currentSubscription is null)
            {
                SubscriptionId id = new SubscriptionId(Guid.NewGuid());
                return new Subscription(id, DateTime.UtcNow, SubscriptionTypeEnum.None, false);
            }
            return currentSubscription;
        }

        private void UpdateSubscriptionPaymentStatus(Subscription subscription)
        {
            Subscription sub = GetCurrentSubscription();
            sub.Pay();
        }

        public void UpdateSubscriptionPaymentStatusRange(ICollection<Subscription> subscriptions)
        {
            foreach (Subscription subscription in subscriptions)
            {
                subscription.Pay();
            }
        }

        internal decimal GetPaymentAmmount(ICollection<Subscription> subscriptions)
        {
            ICollection<Subscription> subs = GetActiveSubscriptions(subscriptions);
            decimal amount = 0;
            foreach (Subscription sub in subs)
            {
                amount += sub._subscriptionFee;
            }
            return amount;
        }

        private ICollection<Subscription> GetActiveSubscriptions(ICollection<Subscription> subscriptions)
        {
            ICollection<Subscription> ActiveSubscriptions = new List<Subscription>();

            Subscription currentSubscription = GetCurrentSubscription();
            foreach (Subscription sub in subscriptions)
            {
                if (sub._subStartDate >= currentSubscription._subStartDate)
                    ActiveSubscriptions.Add(sub);
            }
            return ActiveSubscriptions;
        }
    }
}