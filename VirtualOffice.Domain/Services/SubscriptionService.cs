using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.Services
{
    public class SubscriptionService
    {
        private ICollection<Subscription> _Subscriptions { get; set; }

        public SubscriptionService(ICollection<Subscription> subscriptions)
        {
            _Subscriptions = subscriptions;
        }

        private void AddSubscription(Subscription subscription)
        {
            
        }

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

        public void AddSubscriptionRange(ICollection<Subscription> subscription)
        {

        }
        private void UpgradeSubscriptionType(Subscription subscription)
        {

        }
        public void UpgradeSubscriptionTypeRange(ICollection<Subscription> subscription)
        {

        }
        public Subscription GetCurrentSubscription(ICollection<Subscription> subscriptions)
        {
            throw new NotImplementedException();
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
