using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;

namespace VirtualOffice.Domain.Entities
{
    public class EmployeeTask
    {
        public EmployeeTaskId Id { get; private set; }
        public EmployeeTaskTitle _EmployeeTaskTitle {get; private set;}
        public EmployeeTaskDescription _EmployeeTaskDescription {get; private set;}

        // public EmployeeTaskAssignedEmployees {get; private set;}

        // public EmployeeTaskPriority {get; private set;}

        // public EmployeeTaskStartDate {get; private set;}

        // public EmployeeTaskEndDate {get; private set;}

        // public EmployeeTaskStatus NOTE:make this enum {get; private set;}

    }
}
