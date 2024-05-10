using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

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
        /// <param name="user">The ApplicationUser object representing the user.</param>
        /// <param name="endDate">The cutoff date until which tasks should be retrieved.</param>
        /// /// <remarks>
        /// This method filters the collection of employee tasks to include only those assigned to the specified user,
        /// then sorts these tasks based on their priority before converting them into an immutable sorted set.
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



        
    }
}
