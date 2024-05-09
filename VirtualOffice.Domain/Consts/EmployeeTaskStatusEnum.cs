using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Domain.Consts
{
    public enum EmployeeTaskStatusEnum
    {
        NotStarted = 1,
        InProgress = 2,
        AwaitingReview = 3,
        Done = 4,
    }
}
