using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.SubscriptionService;
using VirtualOffice.Domain.ValueObjects.Subscription;

namespace VirtualOffice.Domain.Services
{
    
    public class SubscriptionService
    {
        private ICollection<Subscription> _Subscriptions { get; set; }
        public int SubscriptionsCount => _Subscriptions.Count;
        public SubscriptionService(ICollection<Subscription> subscriptions)
        {
            _Subscriptions = subscriptions;
        }

        public void AddSubscription(Subscription subscription)
        {
            if(!DoesSubInThatPeriodAlreadyExists(subscription))
                _Subscriptions.Add(subscription);
            else
                throw new SubscriptionDatesOverlapException(subscription._subStartDate, subscription._subEndDate);
        }
            

        internal bool DoesSubInThatPeriodAlreadyExists(Subscription subscription)
        {
            bool flag = true;
            foreach(Subscription sub in _Subscriptions)
            {
                if (subscription._subEndDate.Value < sub._subStartDate.Value ||
                    subscription._subStartDate.Value > sub._subEndDate.Value)
                    flag = false;
                else
                    return true;                
            }
            return flag;
        }

        public Subscription GetSubscriptionById(SubscriptionId id) 
            => _Subscriptions.FirstOrDefault(s => s.Id == id) ?? throw new SubscriptionNotFoundException(id);

        public void AddSubscriptionRange(ICollection<Subscription> subscriptions)
        {
            foreach(Subscription sub in subscriptions)
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
            foreach(SubscriptionId id in Ids)
            {
                UpgradeSubscriptionType(id, subscriptionType);
            }
        }

        // consider what to return
        // return type none when higher tiers are not avalible
        public Subscription GetCurrentSubscription()
        {
            Subscription currentSubscription = _Subscriptions.FirstOrDefault(s => DateTime.UtcNow > s._subStartDate.Value && DateTime.UtcNow < s._subEndDate.Value) ?? throw new CurrentSubscriptionNotFoundException();
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
        private decimal GetPaymentAmmount(ICollection<Subscription> subscriptions)
        {
            Subscription sub = GetCurrentSubscription();
            return sub._subscriptionFee * subscriptions.Count();
        }




    }
}
