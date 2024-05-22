using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VirtualOffice.Domain.DomainEvents.CalendarEventEvents;
using VirtualOffice.Domain.DomainEvents.ScheduleItem;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;
using VirtualOffice.Domain.DomainEvents.MeetingEvent;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DomainUnitTests
{
    public class MeetingUnitTest
    {
        private Meeting _Meeting { get; set; }
        private ApplicationUser _ApplicationUser { get; set; }
        private ApplicationUser _ApplicationUser2 { get; set; }
        private ApplicationUser _ApplicationUser3 { get; set; }
        private ApplicationUser UserNotAdded { get; set; }


        public MeetingUnitTest()
        {

            ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "NameOne", "SurnameOne");
            ApplicationUser user2 = new ApplicationUser(Guid.NewGuid(), "NameTwo", "SurnameTwo");
            ApplicationUser user3 = new ApplicationUser(Guid.NewGuid(), "NameThree", "SurnameThree");
            UserNotAdded = new ApplicationUser(Guid.NewGuid(), "NameFour", "SurnameFour");
            _ApplicationUser = user1;
            _ApplicationUser2 = user2;
            _ApplicationUser3 = user3;

            HashSet<ApplicationUser> users = new HashSet<ApplicationUser>();

            users.Add(user1);
            users.Add(user2);
            users.Add(user3);
            Guid guid = Guid.NewGuid();

            Meeting meeting = new Meeting(guid, "title", "description",
                users, DateTime.UtcNow, DateTime.UtcNow.AddHours(8));

            _Meeting = meeting;
        }

        #region Events
        [Fact]
        public void SetTitle_ShouldRaiseScheduleItemTitleSetted()
        {
            _Meeting.SetTitle("Title");
            var Event = _Meeting.Events.OfType<ScheduleItemTitleSetted>().Single();
        }
        [Fact]
        public void SetTitle_ShouldRaiseScheduleItemTitleSetted_MeetingShouldEqual()
        {
            _Meeting.SetTitle("Title");
            var Event = _Meeting.Events.OfType<ScheduleItemTitleSetted>().Single();
            Assert.Equal(_Meeting, Event.abstractScheduleItem);
        }
        [Fact]
        public void SetTitle_ShouldRaiseScheduleItemTitleSetted_TitleShouldEqual()
        {
            _Meeting.SetTitle("Title");
            var Event = _Meeting.Events.OfType<ScheduleItemTitleSetted>().Single();
            Assert.Equal("Title", Event.title);
        }

        [Fact]
        public void SetDescription_ShouldRaiseScheduleItemDescriptionSetted()
        {
            _Meeting.SetDescription("Description");
            var Event = _Meeting.Events.OfType<ScheduleItemDescriptionSetted>().Single();
        }
        [Fact]
        public void SetDescription_ShouldRaiseScheduleItemDescriptionSetted_MeetingShouldEqual()
        {
            _Meeting.SetDescription("Description");
            var Event = _Meeting.Events.OfType<ScheduleItemDescriptionSetted>().Single();
            Assert.Equal(_Meeting, Event.abstractScheduleItem);
        }
        [Fact]
        public void SetDescription_ShouldRaiseScheduleItemDescriptionSetted_DescriptionShouldEqual()
        {
            _Meeting.SetDescription("Description");
            var Event = _Meeting.Events.OfType<ScheduleItemDescriptionSetted>().Single();
            Assert.Equal("Description", Event.description);
        }
        [Fact]
        public void AddEmployee_ShouldRaiseEmployeeAddedToScheduleItem()
        {
            _Meeting.AddEmployee(UserNotAdded);
            var Event = _Meeting.Events.OfType<EmployeeAddedToScheduleItem>().Single();
        }
        [Fact]
        public void AddEmployee_ShouldRaiseEmployeeAddedToScheduleItem_MeetingShouldEqual()
        {
            _Meeting.AddEmployee(UserNotAdded);
            var Event = _Meeting.Events.OfType<EmployeeAddedToScheduleItem>().Single();
            Assert.Equal(_Meeting, Event.abstractScheduleItem);
        }
        [Fact]
        public void AddEmployee_ShouldRaiseEmployeeAddedToScheduleItem_UserShouldEqual()
        {
            _Meeting.AddEmployee(UserNotAdded);
            var Event = _Meeting.Events.OfType<EmployeeAddedToScheduleItem>().Single();
            Assert.Equal(UserNotAdded, Event.user);
        }
        [Fact]
        public void RemoveEmployee_ShouldRaiseEmployeeRemoverFromScheduleItem()
        {
            _Meeting.RemoveEmployee(_ApplicationUser);
            var Event = _Meeting.Events.OfType<EmployeeRemovedFromScheduleItem>().Single();
        }
        [Fact]
        public void RemoveEmployee_ShouldRaiseEmployeeRemoverFromScheduleItem_MeetingShouldEqual()
        {
            _Meeting.RemoveEmployee(_ApplicationUser);
            var Event = _Meeting.Events.OfType<EmployeeRemovedFromScheduleItem>().Single();
            Assert.Equal(_Meeting, Event.abstractScheduleItem);
        }
        [Fact]
        public void RemoveEmployee_ShouldRaiseEmployeeRemoverFromScheduleItem_UserShouldEqual()
        {
            _Meeting.RemoveEmployee(_ApplicationUser);
            var Event = _Meeting.Events.OfType<EmployeeRemovedFromScheduleItem>().Single();
            Assert.Equal(_ApplicationUser, Event.user);
        }
        [Fact]
        public void UpdateEndDate_ShouldRaiseScheduleItemEndDateUpdate()
        {
            DateTime date = DateTime.UtcNow.AddHours(5);
            _Meeting.UpdateEndDate(date);
            var Event = _Meeting.Events.OfType<ScheduleItemEndDateUpdated>().Single();
        }
        [Fact]
        public void UpdateEndDate_ShouldRaiseScheduleItemEndDateUpdate_MeetingShouldEqual()
        {
            DateTime date = DateTime.UtcNow.AddHours(5);
            _Meeting.UpdateEndDate(date);
            var Event = _Meeting.Events.OfType<ScheduleItemEndDateUpdated>().Single();
            Assert.Equal(_Meeting, Event.abstractScheduleItem);
        }
        [Fact]
        public void UpdateEndDate_ShouldRaiseScheduleItemEndDateUpdate_DateShouldEqual()
        {
            DateTime date = DateTime.UtcNow.AddHours(5);
            _Meeting.UpdateEndDate(date);
            var Event = _Meeting.Events.OfType<ScheduleItemEndDateUpdated>().Single();
            Assert.Equal(date, Event.EndDate);
        }
        [Fact]
        public void UpdateStartDate_ShouldRaiseCalendarEventStartDateUpdate()
        {
            DateTime date = DateTime.UtcNow.AddHours(5);
            _Meeting.UpdateStartDate(date);
            var Event = _Meeting.Events.OfType<MeetingStartDateUpdated>().Single();
        }
        [Fact]
        public void UpdateStartDate_ShouldRaiseCalendarEventStartDateUpdate_MeetingShouldEqual()
        {
            DateTime date = DateTime.UtcNow.AddHours(5);
            _Meeting.UpdateStartDate(date);
            var Event = _Meeting.Events.OfType<MeetingStartDateUpdated>().Single();
            Assert.Equal(_Meeting, Event.meeting);
        }
        [Fact]
        public void UpdateStartDate_ShouldRaiseCalendarEventStartDateUpdate_DateShouldEqual()
        {
            DateTime date = DateTime.UtcNow.AddHours(5);
            _Meeting.UpdateStartDate(date);
            var Event = _Meeting.Events.OfType<MeetingStartDateUpdated>().Single();
            Assert.Equal(date, Event.startDate);
        }
        [Fact]
        public void RescheduleMeeting_ShouldRaiseCalendarEventStartDateUpdate()
        {
            DateTime startDate = DateTime.UtcNow.AddHours(5);
            DateTime endDate = DateTime.UtcNow.AddHours(7);
            _Meeting.RescheduleMeeting(startDate, endDate);
            var Event = _Meeting.Events.OfType<MeetingRescheduled>().Single();
        }
        [Fact]
        public void RescheduleMeeting_ShouldRaiseCalendarEventStartDateUpdate_MeetingShouldEqual()
        {
            DateTime startDate = DateTime.UtcNow.AddHours(5);
            DateTime endDate = DateTime.UtcNow.AddHours(7);
            _Meeting.RescheduleMeeting(startDate, endDate);
            var Event = _Meeting.Events.OfType<MeetingRescheduled>().Single();
            Assert.Equal(_Meeting, Event.Meeting);
        }
        [Fact]
        public void RescheduleMeeting_ShouldRaiseCalendarEventStartDateUpdate_StartDateShouldEqual()
        {
            DateTime startDate = DateTime.UtcNow.AddHours(5);
            DateTime endDate = DateTime.UtcNow.AddHours(7);
            _Meeting.RescheduleMeeting(startDate, endDate);
            var Event = _Meeting.Events.OfType<MeetingRescheduled>().Single();
            Assert.Equal(startDate, Event.startDate);
        }
        [Fact]
        public void RescheduleMeeting_ShouldRaiseCalendarEventStartDateUpdate_EndDateShouldEqual()
        {
            DateTime startDate = DateTime.UtcNow.AddHours(5);
            DateTime endDate = DateTime.UtcNow.AddHours(7);
            _Meeting.RescheduleMeeting(startDate, endDate);
            var Event = _Meeting.Events.OfType<MeetingRescheduled>().Single();
            Assert.Equal(endDate, Event.endDate);
        }
        #endregion

        #region ScheduleItemId

        [Fact]
        public void EmptyScheduleItemId_ShouldReturnEmptyScheduleItemIdException()
        {
            Assert.Throws<EmptyEmployeeScheduleItemIdException>(()
                => new ScheduleItemId(Guid.Empty));
        }
        [Fact]
        public void ValidScheduleItemId_ValidGuidToAppUserIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            ScheduleItemId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidScheduleItemId_ValidAppUserIdToGuidConversionShouldEqual()
        {

            ScheduleItemId id = new ScheduleItemId(Guid.NewGuid());

            Guid guid = id;

            Assert.Equal(id.Value, guid);
        }
        #endregion

        #region ScheduleItemTitle

        [Fact]
        public void EmptyScheduleItemTitle_ShouldReturnEmptyScheduleItemTitleException()
        {
            Assert.Throws<EmptyScheduleItemTitleException>(()
                => new ScheduleItemTitle(""));
        }

        [Fact]
        public void NullScheduleItemTitle_ShouldReturnEmptyScheduleItemTitleException()
        {
            Assert.Throws<EmptyScheduleItemTitleException>(()
                => new ScheduleItemTitle(null));
        }

        [Fact]
        public void ValidScheduleItemTitle_ValidStringToScheduleItemTitleConversion_ShouldEqual()
        {
            string title = "ExampleTitle";

            ScheduleItemTitle tit = title;

            Assert.Equal(title, tit);
        }
        [Fact]
        public void ValidScheduleItemTitle_ValidScheduleItemTitleToStringConversionShouldEqual()
        {
            ScheduleItemTitle tit = new ScheduleItemTitle("ExampleTitle");

            string title = tit;

            Assert.Equal(tit, title);

        }
        [Fact]
        public void InvalidScheduleItemTitle_TooLongTitle()
        {
            string s = new string('a', 101);
            Assert.Throws<TooLongScheduleItemTitleException>(() => new ScheduleItemTitle(s));
        }
        [Fact]
        public void ValidScheduleItemTitle_HundredChars()
        {
            string s = new string('a', 100);
            new ScheduleItemTitle(s);
        }
        [Fact]
        public void ValidScheduleItemTitle_OneChar()
        {
            new ScheduleItemTitle("a");
        }

        #endregion

        #region ScheduleItemDescription 
        [Fact]
        public void ValidScheduleItemDescription_ValidStringToScheduleItemDescriptionConversion_ShouldEqual()
        {
            string Description = "ExampleDescription";

            ScheduleItemDescription desc = Description;

            Assert.Equal(Description, desc);
        }
        [Fact]
        public void ValidScheduleItemDescription_ValidScheduleItemDescriptionToStringConversionShouldEqual()
        {
            ScheduleItemDescription desc = new ScheduleItemDescription("ExampleDescription");

            string Description = desc;

            Assert.Equal(desc, Description);

        }
        [Fact]
        public void InvalidScheduleItemDescription_TooLongDescription()
        {
            string s = new string('a', 1501);
            Assert.Throws<TooLongScheduleItemDescriptionException>(() => new ScheduleItemDescription(s));
        }
        [Fact]
        public void ValidScheduleItemDescription_HundredChars()
        {
            string s = new string('a', 1500);
            new ScheduleItemDescription(s);
        }
        [Fact]
        public void ValidScheduleItemDescription_OneChar()
        {
            new ScheduleItemDescription("a");
        }
        [Fact]
        public void ValidScheduleItemDescription_WhiteSpace()
        {
            new ScheduleItemDescription(" ");
        }
        [Fact]
        public void ValidScheduleItemDescription_Empty()
        {
            new ScheduleItemDescription("");
        }
        #endregion

        #region ScheduleItemStartDate

        [Fact]
        public void ScheduleItemStartDate31daysBeforeNow_ScheduleItemStartDateCannotBePastException()
        {
            Assert.Throws<ScheduleItemStartDateCannotBePastException>(()
                => new ScheduleItemStartDate
                (
                    DateTime.UtcNow.AddDays(-31))
                );
        }
        [Fact]
        public void ScheduleItemStartDateValid31daysFromNow_ShouldNotThrowException()
        {
            ScheduleItemStartDate startDate = DateTime.UtcNow.AddDays(31);
        }
        [Fact]
        public void ScheduleItemStartDateValidYearFromNow_ShouldNotThrowException()
        {
            ScheduleItemStartDate startDate = DateTime.UtcNow.AddYears(1);
        }
        [Fact]
        public void ScheduleItemStartDateValidCurrentTime_ShouldNotThrowException()
        {
            ScheduleItemStartDate startDate = DateTime.UtcNow;
        }

        [Fact]
        public void ValidData_DateTimeToScheduleItemStartDateConversion_ShouldEqual()
        {
            var dt = DateTime.UtcNow.AddDays(31);

            ScheduleItemStartDate startDate = dt;

            Assert.Equal(dt, startDate);
        }
        [Fact]
        public void ValidData_ScheduleItemStartDateToDateTimeConversionShouldEqual()
        {
            ScheduleItemStartDate startDate = new ScheduleItemStartDate(DateTime.UtcNow.AddDays(31));


            var dt = startDate;


            Assert.Equal(startDate, dt);
        }


        #endregion

        #region ScheduleItemEndDate

        [Fact]
        public void ScheduleItemEndDate1dayBeforeNow_ScheduleItemEndDateCannotBePastException()
        {
            Assert.Throws<InvalidScheduleItemEndDateException>(()
                => new ScheduleItemEndDate
                (
                    DateTime.UtcNow.AddDays(-1))
                );
        }
        [Fact]
        public void ScheduleItemEndDateInvalidCurrentTime_ShouldThrowInvalidScheduleItemEndDateException()
        {
            Assert.Throws<InvalidScheduleItemEndDateException>(()
                => new ScheduleItemEndDate
                (
                    DateTime.UtcNow
                ));
        }
        [Fact]
        public void ScheduleItemEndDateValid31daysFromNow_ShouldNotThrowException()
        {
            ScheduleItemEndDate EndDate = DateTime.UtcNow.AddDays(31);
        }
        [Fact]
        public void ScheduleItemEndDateValidYearFromNow_ShouldNotThrowException()
        {
            ScheduleItemEndDate EndDate = DateTime.UtcNow.AddYears(1);
        }


        [Fact]
        public void ValidData_DateTimeToScheduleItemEndDateConversion_ShouldEqual()
        {
            var dt = DateTime.UtcNow.AddDays(31);

            ScheduleItemEndDate EndDate = dt;

            Assert.Equal(dt, EndDate);
        }
        [Fact]
        public void ValidData_ScheduleItemEndDateToDateTimeConversionShouldEqual()
        {
            ScheduleItemEndDate EndDate = new ScheduleItemEndDate(DateTime.UtcNow.AddDays(31));


            var dt = EndDate;


            Assert.Equal(EndDate, dt);
        }
        #endregion

        #region methods
        [Fact]
        public void _Meeting_SetTitle_ProperlySetted()
        {
            string title = "ChangedTitle";
            _Meeting.SetTitle(title);
            Assert.Equal(title, _Meeting._Title);
        }

        [Fact]
        public void _Meeting_SetDescription_ProperlySetted()
        {
            string description = "ChangedDescription";
            _Meeting.SetDescription(description);
            Assert.Equal(description, _Meeting._Description);
        }
        [Fact]
        public void AddEmployee_UserNotAssignedPreviously_ListShouldContainUser()
        {
            ApplicationUser user4 = new ApplicationUser(Guid.NewGuid(), "NameFour", "SurnameFour");
            _Meeting.AddEmployee(user4);
            Assert.Contains(user4,_Meeting._AssignedEmployees);
        }
        [Fact]
        public void AddEmployeesRange_UserNotAssignedPreviously_ListShouldContainUsers()
        {
            ApplicationUser user4 = new ApplicationUser(Guid.NewGuid(), "NameFour", "SurnameFour");
            ApplicationUser user5 = new ApplicationUser(Guid.NewGuid(), "NameFive", "SurnameFive");
            ApplicationUser user6 = new ApplicationUser(Guid.NewGuid(), "NameSix", "SurnameSix");
            List<ApplicationUser> usersList = new List<ApplicationUser>() { user4, user5, user6 };
            _Meeting.AddEmployeesRange(usersList);
            Assert.True(_Meeting._AssignedEmployees.Contains(user4) &&
                _Meeting._AssignedEmployees.Contains(user5) &&
                _Meeting._AssignedEmployees.Contains(user6));
        }

        [Fact]
        public void RemoveEmployee_EmployeeIsAlreadyAssigned_ShouldRemoveEmployee()
        {
            Assert.Contains(_ApplicationUser, _Meeting._AssignedEmployees);
            _Meeting.RemoveEmployee(_ApplicationUser);
            Assert.DoesNotContain(_ApplicationUser, _Meeting._AssignedEmployees);
        }
        [Fact]
        public void RemoveEmployee_EmployeeNotAssigned_ShouldThrowUserIsNotAssignedToThis_MeetingException()
        {
            ApplicationUser user4 = new ApplicationUser(Guid.NewGuid(), "NameFour", "SurnameFour");
            Assert.Throws<UserIsNotAssignedToThisScheduleItemException>(() => _Meeting.RemoveEmployee(user4));
        }
        [Fact]
        public void RemoveEmployeesRange_UsersAssignedPreviously_ListShouldNotContainUsers()
        {
            Assert.True(_Meeting._AssignedEmployees.Contains(_ApplicationUser) &&
                _Meeting._AssignedEmployees.Contains(_ApplicationUser2) &&
                _Meeting._AssignedEmployees.Contains(_ApplicationUser3));

            List<ApplicationUser> usersList = new List<ApplicationUser>() { _ApplicationUser, _ApplicationUser2, _ApplicationUser3 };
            _Meeting.RemoveEmployeesRange(usersList);

            Assert.True(!_Meeting._AssignedEmployees.Contains(_ApplicationUser) &&
                !_Meeting._AssignedEmployees.Contains(_ApplicationUser2) &&
                !_Meeting._AssignedEmployees.Contains(_ApplicationUser3));
        }

        [Fact]
        public void InvalidUpdateEndDate_EndDateIsBeforeStartDate_ShouldThrow_MeetingEndDateCannotBeBeforeStartDate()
        {
            Assert.Throws<EndDateCannotBeBeforeStartDate>(() => _Meeting.UpdateEndDate(_Meeting._StartDate.Value.AddSeconds(-1)));
        }
        [Fact]
        public void ValidUpdateEndDate_EndDateShouldBeEqualToUpdatedDate()
        {
            DateTime dateToUpdate = _Meeting._StartDate.Value.AddSeconds(1);
            _Meeting.UpdateEndDate(dateToUpdate);
            Assert.Equal(dateToUpdate, _Meeting._EndDate);
        }

        #endregion
    }
}


