using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.UserCommands;
using VirtualOffice.Application.Exceptions.ApplicationUser;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.UserHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUser>
    {
        public IUserRepository _repository;
        public IUserReadService _readService;

        public UpdateUserHandler(IUserRepository repository, IUserReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var (Id, Name, Surname) = request;
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new UserDoesNotExistException(request.Id);

            var user = await _repository.GetByIdAsync(Id);

            // we update only changed properties rather than whole object
            // beacuse changing the name to the same name would raise an event.
            if (user._Name != Name)
                user.EditName(Name);
            if (user._Surname != Surname)
                user.EditSurname(Surname);

            await _repository.UpdateAsync(user);
        }
    }
}