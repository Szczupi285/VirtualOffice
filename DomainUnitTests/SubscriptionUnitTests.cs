using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Subscription;
using VirtualOffice.Domain.ValueObjects.Subscription;
using Xunit.Sdk;


namespace DomainUnitTests
{
    public class SubscriptionUnitTests
    {
        #region SubscriptionId

        [Fact]
        public void EmptySubscriptionId_ShouldReturnEmptyApplicationUserIdException()
        {
            Assert.Throws<EmptySubscriptionIdException>(()
                => new SubscriptionId(Guid.Empty));
        }
        [Fact]
        public void ValidSubscriptionId_ValidGuidToSubscriptionIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            SubscriptionId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidSubscription_ValidSubscriptionIdToGuidConversionShouldEqual()
        {

            SubscriptionId id = new SubscriptionId(Guid.NewGuid());

            Guid guid = id;

            Assert.Equal(id.Value, guid);
        }
        #endregion

        #region SubscriptionEndDate
        [Theory]
        [InlineData("1")]
        [InlineData("60")]
        [InlineData("1440")]
        [InlineData("10080")]
        [InlineData("43800")]
        public void SubscriptionEndDateLessThan30daysFromNow_ShouldReturnSubscriptionEndDateInvalidException(string value)
        {
            Assert.Throws<SubscriptionEndDateInvalidException>(()
                => new SubscriptionEndDate
                (
                    DateTime.UtcNow.AddDays(30).AddMinutes(-Convert.ToInt32(value))
                ));
        }
        [Fact]
        public void SubsciptionEndDateValid31daysFromNow_ShouldNotThrowException()
        {
            SubscriptionEndDate endDate = DateTime.UtcNow.AddDays(31);
        }
        [Fact]
        public void SubsciptionEndDateValidYearFromNow_ShouldNotThrowException()
        {
            SubscriptionEndDate endDate = DateTime.UtcNow.AddYears(1);
        }

        [Fact]
        public void ValidData_DateTimeToSubscriptionEndDateConversion_ShouldEqual()
        {
            var dt = DateTime.UtcNow.AddDays(31);

            SubscriptionEndDate endDate = dt;

            Assert.Equal(dt, endDate);
        }
        [Fact]
        public void ValidData_SubscriptionEndDateToDateTimeConversionShouldEqual()
        {
            SubscriptionEndDate endDate = new SubscriptionEndDate(DateTime.UtcNow.AddDays(31));


            var dt = endDate;


            Assert.Equal(endDate, dt);
        }

        [Fact]
        public void SubscriptionEndDate_LessThanOperator_CompareLessThan_ReturnsTrue()
        {
            // Arrange
            SubscriptionEndDate endDate1 = new SubscriptionEndDate(DateTime.UtcNow.AddDays(32)); 
            SubscriptionEndDate endDate2 = new SubscriptionEndDate(DateTime.UtcNow.AddDays(45)); 

            // Act & Assert
            Assert.True(endDate1 < endDate2);
        }

        [Fact]
        public void SubscriptionEndDate_GreaterThanOperator_CompareGreaterThan_ReturnsTrue()
        {
            // Arrange
            SubscriptionEndDate endDate1 = new SubscriptionEndDate(DateTime.UtcNow.AddDays(45)); 
            SubscriptionEndDate endDate2 = new SubscriptionEndDate(DateTime.UtcNow.AddDays(32)); 

            // Act & Assert
            Assert.True(endDate1 > endDate2);
        }

        [Fact]
        public void SubscriptionEndDate_LessThanOrEqualOperator_CompareLessThanOrEqualTo_ReturnsTrue()
        {
            // Arrange
            SubscriptionEndDate endDate1 = new SubscriptionEndDate(DateTime.UtcNow.AddDays(32)); 
            SubscriptionEndDate endDate2 = new SubscriptionEndDate(DateTime.UtcNow.AddDays(45)); 

            // Act & Assert
            Assert.True(endDate1 <= endDate2);
        }

        [Fact]
        public void SubscriptionEndDate_GreaterThanOrEqualOperator_CompareGreaterThanOrEqualTo_ReturnsTrue()
        {
            // Arrange
            SubscriptionEndDate endDate1 = new SubscriptionEndDate(DateTime.UtcNow.AddDays(45)); 
            SubscriptionEndDate endDate2 = new SubscriptionEndDate(DateTime.UtcNow.AddDays(32)); 

            // Act & Assert
            Assert.True(endDate1 >= endDate2);
        }

        [Fact]
        public void SubscriptionEndDate_CompareTo_Equal_ReturnsZero()
        {
            // Arrange
            DateTime date = DateTime.UtcNow.AddDays(32);
            SubscriptionEndDate endDate1 = new SubscriptionEndDate(date);
            SubscriptionEndDate endDate2 = new SubscriptionEndDate(date);

            // Act
            int result = endDate1.CompareTo(endDate2);

            // Assert
            Assert.Equal(0, result);
        }
        #endregion

        #region SubscriptionStartDate

        [Theory]
        [InlineData("1")]
        [InlineData("60")]
        [InlineData("1440")]
        [InlineData("10080")]
        [InlineData("43800")]
        public void SubscriptionStartDate31daysBeforeNow_SubscriptionStartDateCannotBePastException(string value)
        {
            Assert.Throws<SubscriptionStartDateCannotBePastException>(()
                => new SubscriptionStartDate
                (
                    DateTime.UtcNow.AddDays(-31))
                );
        }
        [Fact]
        public void SubsciptionStartDateValid31daysFromNow_ShouldNotThrowException()
        {
            SubscriptionStartDate startDate = DateTime.UtcNow.AddDays(31);
        }
        [Fact]
        public void SubsciptionStartDateValidYearFromNow_ShouldNotThrowException()
        {
            SubscriptionStartDate startDate = DateTime.UtcNow.AddYears(1);
        }
        [Fact]
        public void SubsciptionStartDateValidCurrentTime_ShouldNotThrowException()
        {
            SubscriptionStartDate startDate = DateTime.UtcNow;
        }

        [Fact]
        public void ValidData_DateTimeToSubscriptionStartDateConversion_ShouldEqual()
        {
            var dt = DateTime.UtcNow.AddDays(31);

            SubscriptionStartDate startDate = dt;

            Assert.Equal(dt, startDate);
        }
        [Fact]
        public void ValidData_SubscriptionStartDateToDateTimeConversionShouldEqual()
        {
            SubscriptionStartDate startDate = new SubscriptionStartDate(DateTime.UtcNow.AddDays(31));


            var dt = startDate;


            Assert.Equal(startDate, dt);
        }

        [Fact]
        public void SubscriptionStartDate_LessThanOperator_CompareLessThan_ReturnsTrue()
        {
            // Arrange
            SubscriptionStartDate StartDate1 = new SubscriptionStartDate(DateTime.UtcNow.AddDays(32));
            SubscriptionStartDate StartDate2 = new SubscriptionStartDate(DateTime.UtcNow.AddDays(45));

            // Act & Assert
            Assert.True(StartDate1 < StartDate2);
        }

        [Fact]
        public void SubscriptionStartDate_GreaterThanOperator_CompareGreaterThan_ReturnsTrue()
        {
            // Arrange
            SubscriptionStartDate StartDate1 = new SubscriptionStartDate(DateTime.UtcNow.AddDays(45));
            SubscriptionStartDate StartDate2 = new SubscriptionStartDate(DateTime.UtcNow.AddDays(32));

            // Act & Assert
            Assert.True(StartDate1 > StartDate2);
        }

        [Fact]
        public void SubscriptionStartDate_LessThanOrEqualOperator_CompareLessThanOrEqualTo_ReturnsTrue()
        {
            // Arrange
            SubscriptionStartDate StartDate1 = new SubscriptionStartDate(DateTime.UtcNow.AddDays(32));
            SubscriptionStartDate StartDate2 = new SubscriptionStartDate(DateTime.UtcNow.AddDays(45));

            // Act & Assert
            Assert.True(StartDate1 <= StartDate2);
        }

        [Fact]
        public void SubscriptionStartDate_GreaterThanOrEqualOperator_CompareGreaterThanOrEqualTo_ReturnsTrue()
        {
            // Arrange
            SubscriptionStartDate StartDate1 = new SubscriptionStartDate(DateTime.UtcNow.AddDays(45));
            SubscriptionStartDate StartDate2 = new SubscriptionStartDate(DateTime.UtcNow.AddDays(32));

            // Act & Assert
            Assert.True(StartDate1 >= StartDate2);
        }

        [Fact]
        public void SubscriptionStartDate_CompareTo_Equal_ReturnsZero()
        {
            // Arrange
            DateTime date = DateTime.UtcNow.AddDays(32);
            SubscriptionStartDate StartDate1 = new SubscriptionStartDate(date);
            SubscriptionStartDate StartDate2 = new SubscriptionStartDate(date);

            // Act
            int result = StartDate1.CompareTo(StartDate2);

            // Assert
            Assert.Equal(0, result);
        }

        #endregion

        #region SubscriptionFee

        static SubscriptionId id = new SubscriptionId(Guid.NewGuid());
        static SubscriptionStartDate startDate = new SubscriptionStartDate(DateTime.UtcNow);        
        static bool isPayed = false;

        [Fact]
        public void SubscriptionFee_NoneSubscription_ReturnsCorrectValue()
        {
            SubscriptionTypeEnum type = SubscriptionTypeEnum.None;

            Subscription subscription = new Subscription(id, startDate, type, isPayed);

            Assert.Equal(0, subscription._subscriptionFee);
        }

        [Fact]
        public void SubscriptionFee_TrialSubscription_ReturnsCorrectValue()
        {
            SubscriptionTypeEnum type = SubscriptionTypeEnum.Trial;

            Subscription subscription = new Subscription(id, startDate, type, isPayed);

            Assert.Equal(0, subscription._subscriptionFee);
        }

        [Fact]
        public void SubscriptionFee_BasicSubscription_ReturnsCorrectValue()
        {
            SubscriptionTypeEnum type = SubscriptionTypeEnum.Basic;

            Subscription subscription = new Subscription(id, startDate, type, isPayed);

            Assert.Equal(100, subscription._subscriptionFee);
        }

        [Fact]
        public void SubscriptionFee_EnterpriseSubscription_ReturnsCorrectValue()
        {
            SubscriptionTypeEnum type = SubscriptionTypeEnum.Enterprise;

            Subscription subscription = new Subscription(id, startDate, type, isPayed);

            Assert.Equal(200, subscription._subscriptionFee);
        }

        [Fact]
        public void SubscriptionFee_PremiumSubscription_ReturnsCorrectValue()
        {
            SubscriptionTypeEnum type = SubscriptionTypeEnum.Premium;

            Subscription subscription = new Subscription(id, startDate, type, isPayed);

            Assert.Equal(350, subscription._subscriptionFee);
        }

        [Fact]
        public void SubscriptionFee_UnlimitedSubscription_ReturnsCorrectValue()
        {
            SubscriptionTypeEnum type = SubscriptionTypeEnum.Unlimited;

            Subscription subscription = new Subscription(id, startDate, type, isPayed);

            Assert.Equal(600, subscription._subscriptionFee);
        }
        #endregion
    }
}
