using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.SubscriptionService;
using VirtualOffice.Domain.Services;
using VirtualOffice.Domain.ValueObjects.Subscription;

namespace DomainUnitTests
{
    public class SubscriptionServiceUnitTests
    {
        private SubscriptionService _subscriptionService { get; set; }

        static SubscriptionId id = new SubscriptionId(Guid.NewGuid());
        static SubscriptionStartDate startDate = new SubscriptionStartDate(DateTime.UtcNow);
        static SubscriptionTypeEnum type = SubscriptionTypeEnum.Basic;
        static bool isPayed = false;
        Subscription subscription = new Subscription(id, startDate, type, isPayed);

        static SubscriptionId id2 = new SubscriptionId(Guid.NewGuid());
        static SubscriptionStartDate startDate2 = new SubscriptionStartDate(DateTime.UtcNow.AddDays(35));
        static SubscriptionTypeEnum type2 = SubscriptionTypeEnum.Basic;
        static bool isPayed2 = false;
        Subscription subscription2 = new Subscription(id2, startDate2, type2, isPayed2);

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
            Subscription sub = new Subscription(id2, startDate2.Value.AddDays(31), type2, isPayed2);
            _subscriptionService.AddSubscription(sub);

            Assert.Equal(3, _subscriptionService.SubscriptionsCount);
        }

        [Fact]
        public void DoesSubInThatPeriodAlreadyExists_SubscriptionTypeNone_ShouldReturnFalse()
        {
            subscription.UpdateSubType(SubscriptionTypeEnum.None);
            Assert.False(_subscriptionService.DoesSubInThatPeriodAlreadyExists(subscription));
        }

        [Fact]
        public void DoesSubInThatPeriodAlreadyExists_CurrentSubscription_ShouldReturnTrue()
        {
            subscription.UpdateSubType(SubscriptionTypeEnum.Basic);
            Assert.True(_subscriptionService.DoesSubInThatPeriodAlreadyExists(subscription));
        }

        [Fact]
        public void DoesSubInThatPeriodAlreadyExists_StartDateOverlaps_ShouldReturnTrue()
        {
            Subscription sub = new Subscription(id, startDate.Value.AddDays(1), SubscriptionTypeEnum.Basic, isPayed);
            Assert.True(_subscriptionService.DoesSubInThatPeriodAlreadyExists(sub));
        }

        [Fact]
        public void DoesSubInThatPeriodAlreadyExists_EndDateOverlaps_ShouldReturnTrue()
        {
            Subscription sub2 = new Subscription(id, startDate.Value.AddDays(31), SubscriptionTypeEnum.Basic, isPayed);
            Assert.True(_subscriptionService.DoesSubInThatPeriodAlreadyExists(sub2));
        }

        [Fact]
        public void DoesSubInThatPeriodAlreadyExists_ShouldReturnFalse()
        {
            Subscription sub = new Subscription(id, startDate2.Value.AddDays(31), type, isPayed);
            Assert.False(_subscriptionService.DoesSubInThatPeriodAlreadyExists(sub));
        }

        [Fact]
        public void GetSubscriptionById_SubscriptionNotFound_ShouldReturnSubscriptionNotFoundException()
        {
            SubscriptionId id = new SubscriptionId(Guid.NewGuid());
            Assert.Throws<SubscriptionNotFoundException>(() => _subscriptionService.GetSubscriptionById(id));
        }

        [Fact]
        public void GetSubscriptionById_SubscriptionNotFound_ShouldReturnSubscription()
        {
            Assert.Equal(subscription, _subscriptionService.GetSubscriptionById(id));
        }

        [Fact]
        public void AddSubscriptionRange_ShouldAddSubscription()
        {
            SubscriptionStartDate start1 = new SubscriptionStartDate(startDate2.Value.AddDays(31));
            SubscriptionStartDate start2 = new SubscriptionStartDate(start1.Value.AddDays(31));

            Subscription subscription1 = new Subscription(id, start1, type, isPayed);
            Subscription subscription2 = new Subscription(id, start2, type, isPayed);

            ICollection<Subscription> subscriptions = new List<Subscription>();
            subscriptions.Add(subscription1);
            subscriptions.Add(subscription2);

            _subscriptionService.AddSubscriptionRange(subscriptions);

            Assert.Equal(4, _subscriptionService.SubscriptionsCount);
        }

        [Fact]
        public void UpgradeSubscriptionTypeRange_ShouldUpgradeSubscription()
        {
            ICollection<SubscriptionId> subscriptionIds = new List<SubscriptionId>();
            subscriptionIds.Add(subscription.Id);

            _subscriptionService.UpgradeSubscriptionTypeRange(subscriptionIds, SubscriptionTypeEnum.Enterprise);

            Assert.Equal(SubscriptionTypeEnum.Enterprise, subscription._subType);
        }

        [Fact]
        public void UpgradeSubscriptionTypeRange_ShouldReturnSubscriptionTypeCannotBeChangedToLowerTierException()
        {
            ICollection<SubscriptionId> subscriptionIds = new List<SubscriptionId>();
            subscriptionIds.Add(subscription.Id);

            Assert.Throws<SubscriptionTypeCannotBeChangedToLowerTier>(() => _subscriptionService.UpgradeSubscriptionTypeRange(subscriptionIds, SubscriptionTypeEnum.None));
        }

        [Fact]
        public void UpdateSubscriptionPaymentStatusRange_ShouldReturnTrue()
        {
            ICollection<Subscription> subscriptions = new List<Subscription>();
            subscriptions.Add(subscription);

            _subscriptionService.UpdateSubscriptionPaymentStatusRange(subscriptions);

            Assert.True(subscription._isPayed);
        }

        [Fact]
        public void GetPaymentAmmount_AmountShouldMatch()
        {
            SubscriptionStartDate start1 = new SubscriptionStartDate(DateTime.UtcNow);
            SubscriptionStartDate start2 = new SubscriptionStartDate(start1.Value.AddDays(31));

            Subscription subscription1 = new Subscription(id, start1, SubscriptionTypeEnum.Basic, isPayed); // for 100
            Subscription subscription2 = new Subscription(id, start2, SubscriptionTypeEnum.Enterprise, isPayed); // for 200

            ICollection<Subscription> subscriptions = new List<Subscription>();
            subscriptions.Add(subscription1);
            subscriptions.Add(subscription2);

            Assert.Equal(2, subscriptions.Count);
            Assert.Equal(300, _subscriptionService.GetPaymentAmmount(subscriptions));
        }

        [Fact]
        public void GetCurrentSubscription_ShouldReturnSubscription()
        {
            Assert.Equal(subscription, _subscriptionService.GetCurrentSubscription());
        }

        [Fact]
        public void GetCurrentSubscription_ShouldReturnCurrentSubscriptionNotFoundException()
        {
            ICollection<Subscription> subscriptions = new List<Subscription>();
            _subscriptionService = new SubscriptionService(subscriptions);

            Assert.Throws<CurrentSubscriptionNotFoundException>(() => _subscriptionService.GetCurrentSubscription());
        }

    }
}
