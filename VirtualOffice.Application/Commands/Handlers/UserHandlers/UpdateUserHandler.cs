﻿using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public UpdateUserHandler(IUserRepository repository, IUserReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
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

                    break;
                }
                catch (DbUpdateConcurrencyException)
                {
                    _retryCount++;

                    // rethrowing exception after max attempts
                    if (_retryCount >= _maxRetryAttempts)
                        throw;

                    // wait for certain ammount of time between entries;
                    await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
                }
            }
        }
    }
}