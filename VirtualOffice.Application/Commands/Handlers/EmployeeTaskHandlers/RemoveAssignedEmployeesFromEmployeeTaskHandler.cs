using MediatR;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Exceptions.EmployeeTask;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers
{
    public class RemoveAssignedEmployeesFromEmployeeTaskHandler : IRequestHandler<RemoveAssignedEmployeesFromEmployeeTask>
    {
        private readonly IEmployeeTaskRepository _repository;
        private readonly IEmployeeTaskReadService _readService;

        public RemoveAssignedEmployeesFromEmployeeTaskHandler(IEmployeeTaskRepository repository, IEmployeeTaskReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(RemoveAssignedEmployeesFromEmployeeTask request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Guid))
                throw new EmployeeTaskDoesNotExistsException(request.Guid);

            var calEv = await _repository.GetByIdAsync(request.Guid);
            calEv.RemoveEmployeesRange(request.EmployeesToRemove);
            await _repository.UpdateAsync(calEv);
        }
    }
}