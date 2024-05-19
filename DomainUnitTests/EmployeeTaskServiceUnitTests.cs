using Moq;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Services;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;
using VirtualOffice.Shared;


namespace DomainUnitTests
{
    public class EmployeeTaskServiceUnitTests
    {
        private EmployeeTaskService service {  get; set; }
        
        private EmployeeTask _Task1 { get; set; }
        private EmployeeTask _Task2 { get; set; }
        private EmployeeTask _Task3 { get; set; }
        private EmployeeTask _Task4 { get; set; }

        private ApplicationUser _ApplicationUser1 { get; set; }
        private ApplicationUser _ApplicationUser2 { get; set; }
        private ApplicationUser _ApplicationUser3 { get; set; }
        private ApplicationUser _ApplicationUser4 { get; set; }

        public EmployeeTaskServiceUnitTests()
        {

            ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "NameOne", "SurnameOne");
            ApplicationUser user2 = new ApplicationUser(Guid.NewGuid(), "NameTwo", "SurnameTwo");
            ApplicationUser user3 = new ApplicationUser(Guid.NewGuid(), "NameThree", "SurnameThree");
            ApplicationUser user4 = new ApplicationUser(Guid.NewGuid(), "NameFour", "SurnameFour");
            _ApplicationUser1 = user1;
            _ApplicationUser2 = user2;
            _ApplicationUser3 = user3;
            _ApplicationUser4 = user4;
            HashSet<ApplicationUser> applicationUsers1 = new HashSet<ApplicationUser>() {user1, user2 };
            HashSet<ApplicationUser> applicationUsers2 = new HashSet<ApplicationUser>() {user3 };

            // Intance 1
            Guid task1Guid = Guid.NewGuid();
            var task1Id = new ScheduleItemId(task1Guid);
            var task1Title = new ScheduleItemTitle("Task 1");
            var task1Description = new ScheduleItemDescription("Description for Task 1");
            var assignedEmployees1 = applicationUsers1;
            var priority1 = EmployeeTaskPriorityEnum.High;
            var startDate1 = new ScheduleItemStartDate(DateTime.UtcNow);
            var endDate1 = new ScheduleItemEndDate(DateTime.UtcNow.AddDays(7));

            _Task1 = new EmployeeTask(task1Id, task1Title, task1Description, assignedEmployees1, priority1, startDate1, endDate1);

            // Instance 2
            Guid task2Guid = Guid.NewGuid();
            var task2Id = new ScheduleItemId(task2Guid);
            var task2Title = new ScheduleItemTitle("Task 2");
            var task2Description = new ScheduleItemDescription("Description for Task 2");
            var assignedEmployees2 = applicationUsers1;
            var priority2 = EmployeeTaskPriorityEnum.Medium;
            var startDate2 = new ScheduleItemStartDate(DateTime.UtcNow);
            var endDate2 = new ScheduleItemEndDate(DateTime.UtcNow.AddDays(5));

            _Task2 = new EmployeeTask(task2Id, task2Title, task2Description, assignedEmployees2, priority2, startDate2, endDate2);

            // Instance 3
            Guid task3Guid = Guid.NewGuid();
            var task3Id = new ScheduleItemId(task3Guid);
            var task3Title = new ScheduleItemTitle("Task 3");
            var task3Description = new ScheduleItemDescription("Description for Task 3");
            var assignedEmployees3 = applicationUsers2;
            var priority3 = EmployeeTaskPriorityEnum.Low;
            var startDate3 = new ScheduleItemStartDate(DateTime.UtcNow);
            var endDate3 = new ScheduleItemEndDate(DateTime.UtcNow.AddDays(3));

            _Task3 = new EmployeeTask(task3Id, task3Title, task3Description, assignedEmployees3, priority3, startDate3, endDate3);


            // Instance 4
            Guid task4Guid = Guid.NewGuid();
            var task4Id = new ScheduleItemId(task3Guid);
            var task4Title = new ScheduleItemTitle("Task 4");
            var task4Description = new ScheduleItemDescription("Description for Task 4");
            var assignedEmployees4 = new HashSet<ApplicationUser>();
            var priority4 = EmployeeTaskPriorityEnum.Low;
            var startDate4 = new ScheduleItemStartDate(DateTime.UtcNow);
            var endDate4 = new ScheduleItemEndDate(DateTime.UtcNow.AddDays(3));

            _Task4 = new EmployeeTask(task4Id, task4Title, task4Description, assignedEmployees4, priority4, startDate4, endDate4);

            service = new EmployeeTaskService(new HashSet<EmployeeTask> {_Task1, _Task2, _Task3});
            
        }

        [Fact]
        public void AssignTask_AddsTaskToList()
        {
            service.AssignTask(_Task3);

            Assert.Contains(_Task3, service._EmployeeTasks);
        }

        [Fact]
        public void AssignTask_AddsTaskToList_ShouldReturnTrue()
        {
            Assert.True(service.AssignTask(_Task4));
        }
        [Fact]
        public void AssignTask_AddsTaskToList_ShouldReturnFalse()
        {
            Assert.False(service.AssignTask(_Task1));
        }

        [Fact]
        public void DeleteTask_RemovesTaskFromList()
        {
            service.DeleteTask(_Task1);

            Assert.DoesNotContain(_Task1, service._EmployeeTasks); 
        }
        [Fact]
        public void DeleteTask_RemovesTaskFromList_ShouldReturnTrue()
        {
            Assert.True(service.DeleteTask(_Task1));
        }
        [Fact]
        public void DeleteTask_RemovesTaskFromList_ShouldReturnFalse()
        {
            Assert.False(service.DeleteTask(_Task4));
        }

        [Fact]
        public void GetTaskById_ReturnsTaskIfExists()
        {
            var resultTask = service.GetTaskById(_Task1.Id);

            Assert.Equal(_Task1, resultTask);
        }

        [Fact]
        public void GetTaskById_ReturnsNullForNonExistentId()
        {
            var resultTask = service.GetTaskById(Guid.NewGuid());

            Assert.Null(resultTask);
        }

        [Fact]
        public void GetAllEmployeeTasks_ReturnsExpectedTasksForUser()
        {
            var resultTasks = service.GetAllEmployeeTasks(_ApplicationUser1);

            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
            Assert.DoesNotContain(_Task3, resultTasks);
            Assert.DoesNotContain(_Task4, resultTasks);
        }
        [Fact]
        public void GetAllEmployeeTasks_ReturnsExpectedTasksInGoodOrder()
        {
            var resultTasks = service.GetAllEmployeeTasks(_ApplicationUser1);

            Assert.True(resultTasks[0] ==  _Task1);
            Assert.True(resultTasks[1] ==  _Task2);
        }

        [Fact]
        public void GetAllEmployeeTasks_ReturnsEmptySetForNonAssignedUser()
        {
            var resultTasks = service.GetAllEmployeeTasks(_ApplicationUser4);

            Assert.Empty(resultTasks);
        }

        [Fact]
        public void GetAllEmployeeTasksForUsersGroup_ReturnsExpectedTasksForUsers()
        {
            var resultTasks = service.GetAllEmployeeTasksForUsersGroup(new List<ApplicationUser>() { _ApplicationUser1, _ApplicationUser3 });
            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
            Assert.Contains(_Task3, resultTasks);
        }
        [Fact]
        public void GetAllEmployeeTasksForUsersGroup_ReturnsEmptySetForNonAssignedUser()
        {
            var resultTasks = service.GetAllEmployeeTasksForUsersGroup(new List<ApplicationUser>() { _ApplicationUser4});
            Assert.Empty(resultTasks);
        }
        [Fact]
        public void GetAllEmployeeTasksForUsersGroup_ReturnsExpectedTasksInGoodOrder()
        {
            var resultTasks = service.GetAllEmployeeTasksForUsersGroup(new List<ApplicationUser>() { _ApplicationUser1, _ApplicationUser2 });

            Assert.True(resultTasks[0] == _Task1);
            Assert.True(resultTasks[1] == _Task2);
        }
        [Fact]
        public void GetEmployeeTasksUntilDate_ReturnsExpectedTasksForUsers()
        {
            var resultTasks = service.GetEmployeeTasksUntilDate(_ApplicationUser1, DateTime.UtcNow.AddDays(6));
            Assert.Contains(_Task2, resultTasks);

        }
        [Fact]
        public void GetEmployeeTasksUntilDate_ReturnsExpectedTasksInGoodOrder()
        {
            var resultTasks = service.GetEmployeeTasksUntilDate(_ApplicationUser1, DateTime.UtcNow.AddDays(10));

            Assert.True(resultTasks[0] == _Task1);
            Assert.True(resultTasks[1] == _Task2);
        }
        [Fact]
        public void GetEmployeeTasksUntilDate_ReturnsEmptySetForNotFoundTasks()
        {
            var resultTasks = service.GetEmployeeTasksUntilDate(_ApplicationUser1, DateTime.UtcNow.AddDays(1));
            Assert.Empty(resultTasks);
        }

        [Fact]
        public void GetEmployeeTasksByStatus_ReturnsExpectedTasksInGoodOrder()
        {
            var resultTasks = service.GetEmployeeTasksByStatus(_ApplicationUser1, EmployeeTaskStatusEnum.NotStarted);

            Assert.True(resultTasks[0] == _Task1);
            Assert.True(resultTasks[1] == _Task2);
        }
        [Fact]
        public void GetEmployeeTasksByStatus_ReturnsOneTaskWithTaskStatusNotStarted()
        {
            _Task1.UpdateStatus(EmployeeTaskStatusEnum.InProgress);
            var resultTasks = service.GetEmployeeTasksByStatus(_ApplicationUser1, EmployeeTaskStatusEnum.NotStarted);
            Assert.Contains(_Task2, resultTasks);
        }
        [Fact]
        public void GetEmployeeTasksByStatus_ReturnsOneTaskWithTaskStatusInProgress()
        {
            _Task1.UpdateStatus(EmployeeTaskStatusEnum.InProgress);
            var resultTasks = service.GetEmployeeTasksByStatus(_ApplicationUser1, EmployeeTaskStatusEnum.InProgress);
            Assert.Contains(_Task1, resultTasks);
        }
        [Fact]
        public void GetEmployeeTasksByStatus_ReturnsOneTaskWithTaskStatusAwaitingReview()
        {
            _Task1.UpdateStatus(EmployeeTaskStatusEnum.AwaitingReview);
            var resultTasks = service.GetEmployeeTasksByStatus(_ApplicationUser1, EmployeeTaskStatusEnum.AwaitingReview);
            Assert.Contains(_Task1, resultTasks);
        }
        [Fact]
        public void GetEmployeeTasksByStatus_ReturnsOneTaskWithTaskStatusDone()
        {
            _Task1.UpdateStatus(EmployeeTaskStatusEnum.Done);
            var resultTasks = service.GetEmployeeTasksByStatus(_ApplicationUser1, EmployeeTaskStatusEnum.Done);
            Assert.Contains(_Task1, resultTasks);
        }
        [Fact]
        public void GetEmployeeTasksByStatus_ReturnsTasksWithTaskStatusNotStarted()
        {
            var resultTasks = service.GetEmployeeTasksByStatus(_ApplicationUser1, EmployeeTaskStatusEnum.NotStarted);
            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }
        [Fact]
        public void GetEmployeeTasksByStatus_ReturnsTasksWithTaskStatusInProgress()
        {
            _Task1.UpdateStatus(EmployeeTaskStatusEnum.InProgress);
            _Task2.UpdateStatus(EmployeeTaskStatusEnum.InProgress);
            var resultTasks = service.GetEmployeeTasksByStatus(_ApplicationUser1, EmployeeTaskStatusEnum.InProgress);
            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }
        [Fact]
        public void GetEmployeeTasksByStatus_ReturnsTasksWithTaskStatusAwaitingReview()
        {
            _Task1.UpdateStatus(EmployeeTaskStatusEnum.AwaitingReview);
            _Task2.UpdateStatus(EmployeeTaskStatusEnum.AwaitingReview);
            var resultTasks = service.GetEmployeeTasksByStatus(_ApplicationUser1, EmployeeTaskStatusEnum.AwaitingReview);
            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }
        [Fact]
        public void GetEmployeeTasksByStatus_ReturnsTasksWithTaskStatusDone()
        {
            _Task1.UpdateStatus(EmployeeTaskStatusEnum.Done);
            _Task2.UpdateStatus(EmployeeTaskStatusEnum.Done);
            var resultTasks = service.GetEmployeeTasksByStatus(_ApplicationUser1, EmployeeTaskStatusEnum.Done);
            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }
        
        [Fact]
        public void GetEmployeeTasksByPriority_ReturnsTasksWithPriorityUrgent()
        {
            _Task1.SetPriority(EmployeeTaskPriorityEnum.Urgent);
            var resultTasks = service.GetEmployeeTasksByPriority(_ApplicationUser1, EmployeeTaskPriorityEnum.Urgent);
            Assert.Contains(_Task1, resultTasks);
        }
        [Fact]
        public void GetEmployeeTasksByPriority_ReturnsTasksWithPriorityHigh()
        {
            // task 1 is set to high in constructor
            var resultTasks = service.GetEmployeeTasksByPriority(_ApplicationUser1, EmployeeTaskPriorityEnum.High);
            Assert.Contains(_Task1, resultTasks);
        }
        [Fact]
        public void GetEmployeeTasksByPriority_ReturnsTasksWithPriorityMedium()
        {
            // task 2 is set to medium in constructor
            var resultTasks = service.GetEmployeeTasksByPriority(_ApplicationUser1, EmployeeTaskPriorityEnum.Medium);
            Assert.Contains(_Task2, resultTasks);
        }
        [Fact]
        public void GetEmployeeTasksByPriority_ReturnsTasksWithPriorityLow()
        {
            _Task1.SetPriority(EmployeeTaskPriorityEnum.Low);
            var resultTasks = service.GetEmployeeTasksByPriority(_ApplicationUser1, EmployeeTaskPriorityEnum.Low);
            Assert.Contains(_Task1, resultTasks);
        }
        [Fact]
        public void GetEmployeeTasksByPriority_ReturnsEmptySetForNotFoundTasks()
        {
            var resultTasks = service.GetEmployeeTasksByPriority(_ApplicationUser1, EmployeeTaskPriorityEnum.Low);
            Assert.Empty(resultTasks);
        }

        [Fact]
        public void GetOverdueTasks_ReturnsEmptySetForNotFoundTasks()
        {
            var resultTasks = service.GetOverdueTasks(_ApplicationUser1);
            Assert.Empty(resultTasks);
        }
        [Fact]
        public void GetOverdueTasks_ReturnsExpectedTasksInGoodOrder()
        {
            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddDays(10));

            service = new EmployeeTaskService(new HashSet<EmployeeTask> { _Task1, _Task2, _Task3 }, mock.Object);


            var resultTasks = service.GetOverdueTasks(_ApplicationUser1);

            Assert.True(resultTasks[0] == _Task1);
            Assert.True(resultTasks[1] == _Task2);
        }
        [Fact]
        public void GetOverdueTasks_ReturnsExpectedTasksForUsers()
        {

            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddDays(6));

            service = new EmployeeTaskService(new HashSet<EmployeeTask> { _Task1, _Task2, _Task3 }, mock.Object);


            var resultTasks = service.GetOverdueTasks(_ApplicationUser1);
            Assert.Contains(_Task2, resultTasks);
            Assert.DoesNotContain(_Task1, resultTasks);
        }
        [Fact]
        public void GetOverdueTasks_ReturnsExpectedTasksForBothUsers()
        {

            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddDays(8));

            service = new EmployeeTaskService(new HashSet<EmployeeTask> { _Task1, _Task2, _Task3 }, mock.Object);


            var resultTasks = service.GetOverdueTasks(_ApplicationUser1);
            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }

        [Fact]
        public void GetCurrentTasks_ReturnsExpectedTasksInGoodOrder()
        {
            var resultTasks = service.GetCurrentTasks(_ApplicationUser1);

            Assert.True(resultTasks[0] == _Task1);
            Assert.True(resultTasks[1] == _Task2);
        }
        [Fact]
        public void GetCurrentTasks_ReturnsExpectedTasksForUser()
        {

            var resultTasks = service.GetCurrentTasks(_ApplicationUser1);

            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }
        [Fact]
        public void GetCurrentTasks_ReturnsEmptySetForNotFoundTasks()
        {
            _Task1.UpdateStatus(EmployeeTaskStatusEnum.Done);
            _Task2.UpdateStatus(EmployeeTaskStatusEnum.Done);

            var resultTasks = service.GetCurrentTasks(_ApplicationUser1);

            Assert.Empty(resultTasks);
        }
        [Fact]
        public void GetCurrentTasks_ReturnsEmptySetForNotFoundExpiredTasks()
        {
            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddDays(8));

            service = new EmployeeTaskService(new HashSet<EmployeeTask> { _Task1, _Task2, _Task3 }, mock.Object);

            _Task1.UpdateStatus(EmployeeTaskStatusEnum.Done);
            _Task2.UpdateStatus(EmployeeTaskStatusEnum.Done);

            var resultTasks = service.GetCurrentTasks(_ApplicationUser1);

            Assert.Empty(resultTasks);
        }
        [Fact]
        public void GetCurrentTasks_NotStarted_ExpiredTasks_ShouldContainTasks()
        {
            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddDays(8));

            service = new EmployeeTaskService(new HashSet<EmployeeTask> { _Task1, _Task2, _Task3 }, mock.Object);

            var resultTasks = service.GetCurrentTasks(_ApplicationUser1);

            Assert.Contains(_Task1,resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }
        [Fact]
        public void GetCurrentTasks_NotStarted_NotExpiredTasks_ShouldContainTasks()
        {

            var resultTasks = service.GetCurrentTasks(_ApplicationUser1);

            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }
        [Fact]
        public void GetCurrentTasks_TasksInProgress_ExpiredTasks_ShouldContainTasks()
        {
            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddDays(8));

            service = new EmployeeTaskService(new HashSet<EmployeeTask> { _Task1, _Task2, _Task3 }, mock.Object);

            _Task1.UpdateStatus(EmployeeTaskStatusEnum.InProgress);
            _Task2.UpdateStatus(EmployeeTaskStatusEnum.InProgress);

            var resultTasks = service.GetCurrentTasks(_ApplicationUser1);

            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }

        [Fact]
        public void GetCurrentTasks_TasksInProgress_NotExpiredTasks_ShouldContainTasks()
        {
            _Task1.UpdateStatus(EmployeeTaskStatusEnum.InProgress);
            _Task2.UpdateStatus(EmployeeTaskStatusEnum.InProgress);

            var resultTasks = service.GetCurrentTasks(_ApplicationUser1);

            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }
        [Fact]
        public void GetCurrentTasks_TasksAwaitingReview_ExpiredTasks_ShouldContainTasks()
        {
            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddDays(8));

            service = new EmployeeTaskService(new HashSet<EmployeeTask> { _Task1, _Task2, _Task3 }, mock.Object);

            _Task1.UpdateStatus(EmployeeTaskStatusEnum.AwaitingReview);
            _Task2.UpdateStatus(EmployeeTaskStatusEnum.AwaitingReview);

            var resultTasks = service.GetCurrentTasks(_ApplicationUser1);

            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }

        [Fact]
        public void GetCurrentTasks_TasksAwaitingReview_NotExpiredTasks_ShouldContainTasks()
        {
            _Task1.UpdateStatus(EmployeeTaskStatusEnum.AwaitingReview);
            _Task2.UpdateStatus(EmployeeTaskStatusEnum.AwaitingReview);

            var resultTasks = service.GetCurrentTasks(_ApplicationUser1);

            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }
        [Fact]
        public void GetEmployeeTasksPlannedForFuture_ReturnsExpectedTasksInGoodOrder()
        {
            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddYears(-1));

            service = new EmployeeTaskService(new HashSet<EmployeeTask> { _Task1, _Task2, _Task3 }, mock.Object);

            var resultTasks = service.GetEmployeeTasksPlannedForFuture(_ApplicationUser1);

            Assert.True(resultTasks[0] == _Task1);
            Assert.True(resultTasks[1] == _Task2);
        }
        [Fact]
        public void GetEmployeeTasksPlannedForFuture_ShouldContainTasks()
        {
            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(c => c.UtcNow()).Returns(DateTime.UtcNow.AddYears(-1));

            service = new EmployeeTaskService(new HashSet<EmployeeTask> { _Task1, _Task2, _Task3 }, mock.Object);

            var resultTasks = service.GetEmployeeTasksPlannedForFuture(_ApplicationUser1);

            Assert.Contains(_Task1, resultTasks);
            Assert.Contains(_Task2, resultTasks);
        }

        [Fact]
        public void GetEmployeeTasksPlannedForFuture_ShouldNotContainTasks()
        {
            var resultTasks = service.GetEmployeeTasksPlannedForFuture(_ApplicationUser1);

            Assert.DoesNotContain(_Task1, resultTasks);
            Assert.DoesNotContain(_Task2, resultTasks);
        }



    }
}
