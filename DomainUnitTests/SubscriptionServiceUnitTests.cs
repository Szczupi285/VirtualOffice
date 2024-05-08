using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.NoteService;
using VirtualOffice.Domain.Exceptions.SubscriptionService;
using VirtualOffice.Domain.Services;
using VirtualOffice.Domain.ValueObjects.Subscription;

namespace DomainUnitTests
{
    public class SubscriptionServiceUnitTests
    {        
        private SubscriptionService _subscriptionService { get; set; }
        private Guid _Id { get; set; } = Guid.NewGuid();

        public SubscriptionServiceUnitTests()
        {
            SubscriptionId id = new SubscriptionId(Guid.NewGuid());
            SubscriptionStartDate startDate = new SubscriptionStartDate(DateTime.UtcNow);
            SubscriptionEndDate endDate = new SubscriptionEndDate(DateTime.UtcNow.AddDays(31));
            SubscriptionTypeEnum type = SubscriptionTypeEnum.None;
            SubscriptionFee fee = new SubscriptionFee(0);
            bool isPayed = false;

            Subscription subscription = new Subscription(id, startDate, endDate, type, fee, isPayed);
            ICollection<Subscription> subscriptions = new List<Subscription>();
            subscriptions.Add(subscription);
            _subscriptionService = new SubscriptionService(subscriptions);
        }
        
        [Fact]
        public void AddSubscription_DatesOverlap_ShouldReturnSubscriptionDatesOverlapsException()
        {
            SubscriptionId id = new SubscriptionId(Guid.NewGuid());
            SubscriptionStartDate startDate = new SubscriptionStartDate(DateTime.UtcNow);
            SubscriptionEndDate endDate = new SubscriptionEndDate(DateTime.UtcNow.AddDays(31));
            SubscriptionTypeEnum type = SubscriptionTypeEnum.None;
            SubscriptionFee fee = new SubscriptionFee(0);
            bool isPayed = false;

            Subscription Subscription = new Subscription(id, startDate, endDate, type, fee, isPayed);
            
            Assert.Throws<SubscriptionDatesOverlapException>(() => _subscriptionService.AddSubscription(Subscription));
        }

        [Fact]
        public void AddSubscription_ShouldAddSubscription()
        {
            SubscriptionId id = new SubscriptionId(Guid.NewGuid());
            SubscriptionStartDate startDate = new SubscriptionStartDate(DateTime.UtcNow.AddDays(35));
            SubscriptionEndDate endDate = new SubscriptionEndDate(startDate.Value.AddDays(31));
            SubscriptionTypeEnum type = SubscriptionTypeEnum.None;
            SubscriptionFee fee = new SubscriptionFee(0);
            bool isPayed = false;

            Subscription Subscription = new Subscription(id, startDate, endDate, type, fee, isPayed);

            _subscriptionService.AddSubscription(Subscription);

            Assert.Equal(2, _subscriptionService.SubscriptionsCount);
        }
    }
}
