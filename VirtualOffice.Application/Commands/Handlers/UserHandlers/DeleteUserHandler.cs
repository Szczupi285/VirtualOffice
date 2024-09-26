﻿using MediatR;
using VirtualOffice.Application.Commands.UserCommands;
using VirtualOffice.Application.Exceptions.ApplicationUser;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.UserHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser>
    {
        public IUserRepository _repository;
        public IUserReadService _readService;

        public DeleteUserHandler(IUserRepository repository, IUserReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new UserDoesNotExistException(request.Id);

            var entity = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(entity);
        }
    }
}