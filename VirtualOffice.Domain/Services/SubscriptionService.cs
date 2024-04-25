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

        public SubscriptionService(ICollection<Subscription> subscriptions)
        {
            _Subscriptions = subscriptions;
        }

        private void AddSubscription(Subscription subscription) => _Subscriptions.Add(subscription);
            

        private bool DoesSubInThatPeriodAlreadyExists(Subscription subscription)
        {
            foreach(Subscription sub in _Subscriptions)
            {
                if((subscription._subStartDate < sub._subStartDate &&
                    subscription._subEndDate.Value < sub._subStartDate.Value) ||
                    (subscription._subStartDate.Value > sub._subEndDate.Value &&
                    subscription._subEndDate > sub._subEndDate)
                    )
                {
                    return false;
                }
            }
            return true;
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
        // return none when higher tiers are not avalible
        public Subscription GetCurrentSubscription()
        {
            throw new NotImplementedException();
            //_Subscriptions.FirstOrDefault(s => DateTime.UtcNow > s._subStartDate.Value && DateTime.UtcNow < s._subEndDate.Value);
        }
        private bool PayForSubscription(Subscription subscription) 
        {
            throw new NotImplementedException();
        }
        public bool PayForSubscriptionRange(ICollection<Subscription> subscriptions)
        {
            throw new NotImplementedException();
        }
        private decimal GetPaymentAmmount(ICollection<Subscription> subscriptions)
        {
            throw new NotImplementedException();
        }



    }
}
