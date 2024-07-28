using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetFutureEmployeeTasksForUser(Guid UserId) : IRequest<IEnumerable<EmployeeTaskTitleDTO>>;
}
