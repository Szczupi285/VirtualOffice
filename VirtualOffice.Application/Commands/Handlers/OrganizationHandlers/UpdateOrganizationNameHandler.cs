using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Application.Exceptions.Organization;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.OrganizationHandlers
{
    public class UpdateOrganizationNameHandler : IRequestHandler<UpdateOrganizationName>
    {
        public IOrganizationRepository _repository;
        public IOrganizationReadService _readService;
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public UpdateOrganizationNameHandler(IOrganizationRepository repository, IOrganizationReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdateOrganizationName request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
                {
                    if (!await _readService.ExistsByIdAsync(request.OrganizationId))
                        throw new OrganizationDoesNotExistsException(request.OrganizationId);

                    var org = await _repository.GetByIdAsync(request.OrganizationId);

                    org.SetName(request.Name);

                    await _repository.UpdateAsync(org);

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