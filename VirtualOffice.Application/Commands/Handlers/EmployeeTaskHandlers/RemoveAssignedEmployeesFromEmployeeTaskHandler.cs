using MediatR;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Exceptions.EmployeeTask;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers
{


    public class RemoveAssignedEmployeesFromEmployeeTaskHandler : IRequestHandler<RemoveAssignedEmployeesToEmployeeTask>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;

        public RemoveAssignedEmployeesFromEmployeeTaskHandler(ICalendarEventRepository repository, ICalendarEventReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(RemoveAssignedEmployeesToEmployeeTask request, CancellationToken cancellationToken)
        {

            if (!await _readService.ExistsByIdAsync(request.Guid))
            {
                throw new EmployeeTaskDoesNotExistsException(request.Guid);
            }

            var calEv = await _repository.GetById(request.Guid);
            calEv.RemoveEmployeesRange(request.EmployeesToRemove);
            await _repository.Update(calEv);
        }
    }
}
