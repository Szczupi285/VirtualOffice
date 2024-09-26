using MediatR;
using VirtualOffice.Application.Commands.UserCommands;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.UserHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUser>
    {
        public IUserRepository _repository;

        public CreateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateUser request, CancellationToken cancellationToken)
        {
            ApplicationUser newUser = new(Guid.NewGuid(), request.Name, request.Surname, request.Permissions);

            await _repository.AddAsync(newUser);
        }
    }
}