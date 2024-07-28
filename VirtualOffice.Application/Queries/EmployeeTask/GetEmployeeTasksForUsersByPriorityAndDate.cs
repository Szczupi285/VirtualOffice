using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetEmployeeTasksForUsersByPriorityAndDate(Guid UserId, DateTime StartDate, DateTime EndDate, string Priority) : IRequest<IEnumerable<EmployeeTaskTitleDTO>>;
    
}
