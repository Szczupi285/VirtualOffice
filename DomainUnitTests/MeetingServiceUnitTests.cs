using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Services;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VirtualOffice.Shared;
namespace DomainUnitTests
{
    public class MeetingServiceUnitTests
    {
        private MeetingService service { get; set; }

        private Meeting _Meeting1 { get; set; }
        private Meeting _Meeting2 { get; set; }
        private Meeting _Meeting3 { get; set; }
        private Meeting _Meeting4 { get; set; }

        private ApplicationUser _ApplicationUser1 { get; set; }
        private ApplicationUser _ApplicationUser2 { get; set; }
        private ApplicationUser _ApplicationUser3 { get; set; }
        private ApplicationUser _ApplicationUser4 { get; set; }

        public MeetingServiceUnitTests()
        {

            ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "NameOne", "SurnameOne");
            ApplicationUser user2 = new ApplicationUser(Guid.NewGuid(), "NameTwo", "SurnameTwo");
            ApplicationUser user3 = new ApplicationUser(Guid.NewGuid(), "NameThree", "SurnameThree");
            ApplicationUser user4 = new ApplicationUser(Guid.NewGuid(), "NameFour", "SurnameFour");
            _ApplicationUser1 = user1;
            _ApplicationUser2 = user2;
            _ApplicationUser3 = user3;
            _ApplicationUser4 = user4;
            HashSet<ApplicationUser> applicationUsers1 = new HashSet<ApplicationUser>() { user1, user2 };
            HashSet<ApplicationUser> applicationUsers2 = new HashSet<ApplicationUser>() { user3 };

            // Intance 1
            Guid Meeting1Guid = Guid.NewGuid();
            var Meeting1Id = new ScheduleItemId(Meeting1Guid);
            var Meeting1Title = new ScheduleItemTitle("Meeting 1");
            var Meeting1Description = new ScheduleItemDescription("Description for Meeting 1");
            var assignedEmployees1 = applicationUsers1;
            var startDate1 = new ScheduleItemStartDate(DateTime.UtcNow);
            var endDate1 = new ScheduleItemEndDate(DateTime.UtcNow.AddDays(7));

            _Meeting1 = new Meeting(Meeting1Id, Meeting1Title, Meeting1Description, assignedEmployees1, startDate1, endDate1);

            // Instance 2
            Guid Meeting2Guid = Guid.NewGuid();
            var Meeting2Id = new ScheduleItemId(Meeting2Guid);
            var Meeting2Title = new ScheduleItemTitle("Meeting 2");
            var Meeting2Description = new ScheduleItemDescription("Description for Meeting 2");
            var assignedEmployees2 = applicationUsers1;
            var startDate2 = new ScheduleItemStartDate(DateTime.UtcNow);
            var endDate2 = new ScheduleItemEndDate(DateTime.UtcNow.AddDays(5));

            _Meeting2 = new Meeting(Meeting2Id, Meeting2Title, Meeting2Description, assignedEmployees2, startDate2, endDate2);

            // Instance 3
            Guid Meeting3Guid = Guid.NewGuid();
            var Meeting3Id = new ScheduleItemId(Meeting3Guid);
            var Meeting3Title = new ScheduleItemTitle("Meeting 3");
            var Meeting3Description = new ScheduleItemDescription("Description for Meeting 3");
            var assignedEmployees3 = applicationUsers2;
            var startDate3 = new ScheduleItemStartDate(DateTime.UtcNow);
            var endDate3 = new ScheduleItemEndDate(DateTime.UtcNow.AddDays(3));

            _Meeting3 = new Meeting(Meeting3Id, Meeting3Title, Meeting3Description, assignedEmployees3, startDate3, endDate3);


            // Instance 4
            Guid Meeting4Guid = Guid.NewGuid();
            var Meeting4Id = new ScheduleItemId(Meeting3Guid);
            var Meeting4Title = new ScheduleItemTitle("Meeting 4");
            var Meeting4Description = new ScheduleItemDescription("Description for Meeting 4");
            var assignedEmployees4 = new HashSet<ApplicationUser>();
            var startDate4 = new ScheduleItemStartDate(DateTime.UtcNow);
            var endDate4 = new ScheduleItemEndDate(DateTime.UtcNow.AddDays(3));

            _Meeting4 = new Meeting(Meeting4Id, Meeting4Title, Meeting4Description, assignedEmployees4, startDate4, endDate4);

            service = new MeetingService(new HashSet<Meeting> { _Meeting1, _Meeting2, _Meeting3 });

        }

        [Fact]
        public void AssignScheduleItem_AddsMeetingToList()
        {
            service.AssignScheduleItem(_Meeting3);

            Assert.Contains(_Meeting3, service._ScheduleItems);
        }

        [Fact]
        public void AssignScheduleItem_AddsMeetingToList_ShouldReturnTrue()
        {
            Assert.True(service.AssignScheduleItem(_Meeting4));
        }
        [Fact]
        public void AssignScheduleItem_AddsMeetingToList_ShouldReturnFalse()
        {
            Assert.False(service.AssignScheduleItem(_Meeting1));
        }

        [Fact]
        public void DeleteScheduleItem_RemovesMeetingFromList()
        {
            service.DeleteScheduleItem(_Meeting1);

            Assert.DoesNotContain(_Meeting1, service._ScheduleItems);
        }
        [Fact]
        public void DeleteScheduleItem_RemovesMeetingFromList_ShouldReturnTrue()
        {
            Assert.True(service.DeleteScheduleItem(_Meeting1));
        }
        [Fact]
        public void DeleteScheduleItem_RemovesMeetingFromList_ShouldReturnFalse()
        {
            Assert.False(service.DeleteScheduleItem(_Meeting4));
        }

        [Fact]
        public void GetScheduleItemById_ReturnsMeetingIfExists()
        {
            var resultMeeting = service.GetScheduleItemById(_Meeting1.Id);

            Assert.Equal(_Meeting1, resultMeeting);
        }

        [Fact]
        public void GetScheduleItemById_ReturnsNullForNonExistentId()
        {
            var resultMeeting = service.GetScheduleItemById(Guid.NewGuid());

            Assert.Null(resultMeeting);
        }

        [Fact]
        public void GetAllEmployeeMeetings_ShouldContainTwoEmployeeMettings()
        {
            var result = service.GetAllEmployeeMeetings(_ApplicationUser1);

            Assert.Contains(_Meeting1, result);
            Assert.Contains(_Meeting2, result);
            Assert.True(result.Count == 2);
        }
        [Fact]
        public void GetAllEmployeeMeetings_CountShouldBeEqualToTwo()
        {
            var result = service.GetAllEmployeeMeetings(_ApplicationUser1);
            Assert.True(result.Count == 2);
        }
        [Fact]
        public void GetAllEmployeeMeetings_NoMeetingsAssigned_ShouldReturnEmptyCollection()
        {
            var result = service.GetAllEmployeeMeetings(_ApplicationUser4);
            Assert.Empty(result);
        }
        [Fact]
        public void GetAllEmployeeMeetings_MeetingInThePast_ShouldReturnEmptyCollection()
        {
            Mock<IDateTimeProvider> mock = new Mock<IDateTimeProvider>();
            mock.Setup(x => x.UtcNow()).Returns(DateTime.UtcNow.AddMonths(1));
            var serviceInTheFuture = new MeetingService(new HashSet<Meeting> { _Meeting1, _Meeting2, _Meeting3}, mock.Object);
            var result = serviceInTheFuture.GetAllEmployeeMeetings(_ApplicationUser1);
            Assert.Empty(result);
        }
        [Fact]
        public void GetMeetingsUntilDate_ShouldContainMeeting1()
        {
            Mock<IDateTimeProvider> mock = new Mock<IDateTimeProvider>();
            mock.Setup(x => x.UtcNow()).Returns(DateTime.UtcNow.AddDays(6));
            var serviceInTheFuture = new MeetingService(new HashSet<Meeting> { _Meeting1, _Meeting2, _Meeting3 }, mock.Object);
            var result = serviceInTheFuture.GetMeetingsUntilDate(_ApplicationUser1, DateTime.UtcNow.AddDays(8));
            Assert.Contains(_Meeting1, result);
        }
        [Fact]
        public void GetMeetingsUntilDate_ShouldNotContainMetting2()
        {
            Mock<IDateTimeProvider> mock = new Mock<IDateTimeProvider>();
            mock.Setup(x => x.UtcNow()).Returns(DateTime.UtcNow.AddDays(6));
            var serviceInTheFuture = new MeetingService(new HashSet<Meeting> { _Meeting1, _Meeting2, _Meeting3 }, mock.Object);
            var result = serviceInTheFuture.GetMeetingsUntilDate(_ApplicationUser1, DateTime.UtcNow.AddDays(8));
            Assert.DoesNotContain(_Meeting2, result);
        }
        [Fact]
        public void GetMeetingsUntilDate_ShouldContainOnlyOneMeeting()
        {
            Mock<IDateTimeProvider> mock = new Mock<IDateTimeProvider>();
            mock.Setup(x => x.UtcNow()).Returns(DateTime.UtcNow.AddDays(6));
            var serviceInTheFuture = new MeetingService(new HashSet<Meeting> { _Meeting1, _Meeting2, _Meeting3 }, mock.Object);
            var result = serviceInTheFuture.GetMeetingsUntilDate(_ApplicationUser1, DateTime.UtcNow.AddDays(8));
            Assert.Single(result);
        }
    }
}
