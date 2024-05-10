using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;

namespace VirtualOffice.Domain.Services
{
    public class EmployeeTaskService
    {
        private HashSet<EmployeeTask> _EmployeeTasks {get; set;}

        public EmployeeTaskService(HashSet<EmployeeTask> employeeTasks)
        {
            _EmployeeTasks = employeeTasks.ToHashSet();
        }

        public void AssignTask(EmployeeTask task) => _EmployeeTasks.Add(task);   

        public void DeleteTask(EmployeeTask task) => _EmployeeTasks.Remove(task);

        /// <summary>
        /// Retrieves an EmployeeTask by id. Id EmployeeTask is not found, returns null.
        /// </summary>
        public EmployeeTask? GetTaskById(EmployeeTaskId id) => _EmployeeTasks.FirstOrDefault(x => x.Id == id);


        /// <summary>
        /// Retrieves an immutable sorted set of employee tasks assigned to the specified user, sorted by priority.
        /// </summary>
        /// <param name="user">The user for whom to retrieve the employee tasks.</param>
        /// <returns>
        /// An immutable sorted set of employee tasks assigned to the specified user,
        /// sorted by priority in descending order.
        /// </returns>
        /// <remarks>
        /// This method filters the collection of employee tasks to include only those assigned to the specified user,
        /// then sorts these tasks based on their priority before converting them into an immutable sorted set.
        /// </remarks>
        /// <returns>An immutable sorted set of employee tasks assigned to the specified user, sorted by priority.</returns>
        public ImmutableSortedSet<EmployeeTask> GetAllEmployeeTasks(ApplicationUser user)
        {
            return _EmployeeTasks.Where(task => task._AssignedEmployees == user)
                .OrderByDescending(task => task._Priority)
                .ToImmutableSortedSet();
        }

        /// <summary>
        /// Retrieves an immutable sorted set of employee tasks assigned to the specified user until the given date.
        /// </summary>
        /// <param name="user">The user for whom to retrieve the employee tasks.</param>
        /// <param name="endDate">The cutoff date until which tasks should be retrieved.</param>
        /// /// <remarks>
        /// This method filters the collection of employee tasks to include only those assigned to the specified user,
        /// then sorts these tasks based on their priority and endDate before converting them into an immutable sorted set.
        /// </remarks>
        /// <returns>
        /// An ImmutableSortedSet of EmployeeTask objects representing the tasks assigned to the user
        /// until the given date, sorted by priority in descending order.
        /// </returns>
        public ImmutableSortedSet<EmployeeTask> GetEmployeeTasksUntilDate(ApplicationUser user, DateTime endDate)
        {
            return _EmployeeTasks.Where(task => task._AssignedEmployees == user && task._EndDate < endDate)
                .OrderByDescending(task => task._Priority)
                .ToImmutableSortedSet();
        }

        /// <summary>
        /// Retrieves an immutable sorted set of employee tasks assigned to the specified user, sorted by priority and status.
        /// </summary>
        /// <param name="user">The user for whom to retrieve the employee tasks.</param>
        /// <param name="status">The status of the tasks to retrieve.</param>
        /// <returns>
        /// An immutable sorted set of employee tasks assigned to the specified user,
        /// sorted by status and priority in descending order.
        /// </returns>
        /// <remarks>
        /// This method filters the collection of employee tasks to include only those assigned to the specified user
        /// and with a given status. It then sorts these tasks based on their priority before converting them
        /// into an immutable sorted set.
        /// </remarks>
        public ImmutableSortedSet<EmployeeTask> GetEmployeeTasksByStatus(ApplicationUser user, EmployeeTaskStatusEnum status)
        {
            return _EmployeeTasks.Where(task => task._AssignedEmployees == user && task._TaskStatus == status)
                .OrderByDescending(task => task._Priority)
                .ToImmutableSortedSet();
        }

        /// <summary>
        /// Retrieves a HashSet of employee tasks assigned to the specified user with the given priority.
        /// </summary>
        /// <param name="user">The user for whom to retrieve the employee tasks.</param>
        /// <param name="priority">The priority of the tasks to retrieve.</param>
        /// <returns>
        /// A HashSet containing employee tasks assigned to the specified user and having the specified priority.
        /// </returns>
        /// <remarks>
        /// This method filters the collection of employee tasks to include only those assigned to the specified user
        /// and having the specified priority. It then returns these tasks as a HashSet.
        /// </remarks>
        public HashSet<EmployeeTask> GetEmployeeTasksByPriority(ApplicationUser user, EmployeeTaskPriorityEnum priority)
            => _EmployeeTasks.Where(task => task._AssignedEmployees == user && task._Priority == priority).ToHashSet();


        /// <summary>
        /// Retrieves an immutable sorted set of overdue employee tasks assigned to the specified user.
        /// </summary>
        /// <param name="user">The user for whom to retrieve the overdue tasks.</param>
        /// <returns>
        /// An immutable sorted set of overdue employee tasks assigned to the specified user,
        /// sorted by priority in descending order.
        /// </returns>
        /// <remarks>
        /// This method filters the collection of employee tasks to include only those assigned to the specified user
        /// and having an end date earlier than the current UTC time, and the task status is not 'Done'. 
        /// It then sorts these tasks based on their priority before converting them into an immutable sorted set.
        /// </remarks>
        /// <returns>An immutable sorted set of overdue employee tasks assigned to the specified user.</returns>
        public ImmutableSortedSet<EmployeeTask> GetOverdueTasks(ApplicationUser user)
        {
            return _EmployeeTasks.Where(task => task._AssignedEmployees.Contains(user)
            && task._EndDate < DateTime.UtcNow
            && task._TaskStatus != EmployeeTaskStatusEnum.Done)
                .OrderByDescending(task => task._Priority)
                .ToImmutableSortedSet();
        }




    }
}
