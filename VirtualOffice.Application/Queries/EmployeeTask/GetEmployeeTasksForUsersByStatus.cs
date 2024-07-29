﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.EmployeeTask;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetEmployeeTasksForUsersByStatus(Guid UserId, string Status) : IRequest<IEnumerable<EmployeeTaskTitleDTO>>;
}
