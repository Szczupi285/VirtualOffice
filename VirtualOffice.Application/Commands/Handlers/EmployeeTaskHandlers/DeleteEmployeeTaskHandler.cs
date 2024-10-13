using MediatR;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Exceptions.EmployeeTask;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers
{
    public class DeleteEmployeeTaskHandler : IRequestHandler<DeleteEmployeeTask>
    {
        private readonly IEmployeeTaskRepository _repository;
        private readonly IEmployeeTaskReadService _readService;
        private readonly IMediator _mediator;

        public DeleteEmployeeTaskHandler(IEmployeeTaskRepository repository, IEmployeeTaskReadService readService, IMediator mediator)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
        }

        public async Task Handle(DeleteEmployeeTask request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id, cancellationToken))
                throw new EmployeeTaskDoesNotExistsException(request.Id);

            var entity = await _repository.GetByIdAsync(request.Id);
            entity.Disable();

            await _repository.DeleteAsync(entity, cancellationToken);

            foreach (var domainEvent in entity.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            entity.ClearEvents();
        }
    }
}