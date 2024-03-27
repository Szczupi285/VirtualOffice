﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void SubscriptionEndDateLessThan31daysFromNow_ShouldReturnSubscriptionEndDateInvalidException(string value)
        {
            Assert.Throws<SubscriptionEndDateInvalidException>(()
                => new SubscriptionEndDate
                (
                    DateTime.Now.AddDays(31).AddMinutes(-Convert.ToInt32(value))
                ));
        }
        [Fact]
        public void SubsciptionEndDateValid31daysFromNow_ShouldNotThrowException()
        {
            SubscriptionEndDate endDate = DateTime.Now.AddDays(31);
        }
        [Fact]
        public void SubsciptionEndDateValidYearFromNow_ShouldNotThrowException()
        {
            SubscriptionEndDate endDate = DateTime.Now.AddYears(1);
        }

        [Fact]
        public void ValidData_DateTimeToSubscriptionEndDateConversion_ShouldEqual()
        {
            var dt = DateTime.Now.AddDays(31);

            SubscriptionEndDate endDate = dt;

            Assert.Equal(dt, endDate);
        }
        [Fact]
        public void ValidData_SubscriptionEndDateToDateTimeConversionShouldEqual()
        {
            SubscriptionEndDate endDate = new SubscriptionEndDate(DateTime.Now.AddDays(31));


            var dt = endDate;


            Assert.Equal(endDate, dt);
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
                    DateTime.Now.AddDays(-31))
                );
        }
        [Fact]
        public void SubsciptionStartDateValid31daysFromNow_ShouldNotThrowException()
        {
            SubscriptionStartDate startDate = DateTime.Now.AddDays(31);
        }
        [Fact]
        public void SubsciptionStartDateValidYearFromNow_ShouldNotThrowException()
        {
            SubscriptionStartDate startDate = DateTime.Now.AddYears(1);
        }
        [Fact]
        public void SubsciptionStartDateValidCurrentTime_ShouldNotThrowException()
        {
            SubscriptionStartDate startDate = DateTime.Now;
        }

        [Fact]
        public void ValidData_DateTimeToSubscriptionStartDateConversion_ShouldEqual()
        {
            var dt = DateTime.Now.AddDays(31);

            SubscriptionStartDate startDate = dt;

            Assert.Equal(dt, startDate);
        }
        [Fact]
        public void ValidData_SubscriptionStartDateToDateTimeConversionShouldEqual()
        {
            SubscriptionStartDate startDate = new SubscriptionStartDate(DateTime.Now.AddDays(31));


            var dt = startDate;


            Assert.Equal(startDate, dt);
        }

        #endregion
    }
}
