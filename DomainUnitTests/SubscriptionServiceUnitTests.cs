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
        //private Guid _Id { get; set; } = Guid.NewGuid();

        static SubscriptionId id = new SubscriptionId(Guid.NewGuid());
        static SubscriptionStartDate startDate = new SubscriptionStartDate(DateTime.UtcNow);
        static SubscriptionTypeEnum type = SubscriptionTypeEnum.None;
        static SubscriptionFee fee = new SubscriptionFee(0);
        static bool isPayed = false;
        Subscription subscription = new Subscription(id, startDate, type, fee, isPayed);

        static SubscriptionId id2 = new SubscriptionId(Guid.NewGuid());
        static SubscriptionStartDate startDate2 = new SubscriptionStartDate(DateTime.UtcNow.AddDays(35));
        static SubscriptionTypeEnum type2 = SubscriptionTypeEnum.None;
        static SubscriptionFee fee2 = new SubscriptionFee(0);
        static bool isPayed2 = false;
        Subscription subscription2 = new Subscription(id2, startDate2, type2, fee2, isPayed2);

        public SubscriptionServiceUnitTests()
        {          
            ICollection<Subscription> subscriptions = new List<Subscription>();
            subscriptions.Add(subscription);
            subscriptions.Add(subscription2);
            _subscriptionService = new SubscriptionService(subscriptions);
        }
        
        [Fact]
        public void AddSubscription_DatesOverlap_ShouldReturnSubscriptionDatesOverlapsException()
        {            
            Assert.Throws<SubscriptionDatesOverlapException>(() => _subscriptionService.AddSubscription(subscription));
        }

        [Fact]
        public void AddSubscription_ShouldAddSubscription()
        {
            Subscription sub = new Subscription(id2, startDate2.Value.AddDays(31), type2, fee2, isPayed2);
            _subscriptionService.AddSubscription(sub);

            Assert.Equal(3, _subscriptionService.SubscriptionsCount);
        }

        [Fact]
        public void DoesSubInThatPeriodAlreadyExists_ShouldReturnTrue()
        {
            Assert.True(_subscriptionService.DoesSubInThatPeriodAlreadyExists(subscription));
        }

        [Fact]
        public void DoesSubInThatPeriodAlreadyExists_ShouldReturnTrue2()
        {
            Assert.True(_subscriptionService.DoesSubInThatPeriodAlreadyExists(subscription2));

        }
        [Fact]
        public void DoesSubInThatPeriodAlreadyExists_ShouldReturnTrue3()
        {
            Subscription sub = new Subscription(id, startDate.Value.AddDays(1), type, fee, isPayed);
            Assert.True(_subscriptionService.DoesSubInThatPeriodAlreadyExists(sub));
        }
        [Fact]
        public void DoesSubInThatPeriodAlreadyExists_ShouldReturnTrue4()
        {

            Subscription sub2 = new Subscription(id, startDate.Value.AddDays(31), type, fee, isPayed);
            Assert.True(_subscriptionService.DoesSubInThatPeriodAlreadyExists(sub2));
        }



            [Fact]
        public void DoesSubInThatPeriodAlreadyExists_ShouldReturnFalse()
        {
            Subscription sub = new Subscription(id, startDate2.Value.AddDays(31), type, fee, isPayed);
            Assert.False(_subscriptionService.DoesSubInThatPeriodAlreadyExists(sub));
        }
    }
}
