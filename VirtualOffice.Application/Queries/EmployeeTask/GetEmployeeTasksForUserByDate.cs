using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetEmployeeTasksForUserByDate(Guid UserId, DateTime startDate, DateTime endDate) : IRequest<IEnumerable<EmployeeTaskDTO>>;
}
