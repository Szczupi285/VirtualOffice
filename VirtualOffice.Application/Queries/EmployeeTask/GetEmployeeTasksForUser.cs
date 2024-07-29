using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.EmployeeTask;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetEmployeeTasksForUser(Guid UserId) : IRequest<IEnumerable<EmployeeTaskTitleDTO>>;
}
