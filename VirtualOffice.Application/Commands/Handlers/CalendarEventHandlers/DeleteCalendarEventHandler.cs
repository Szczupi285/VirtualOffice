using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    public class DeleteCalendarEventHandler : IRequestHandler<DeleteCalendarEvent>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;

        public DeleteCalendarEventHandler(ICalendarEventRepository repository, ICalendarEventReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(DeleteCalendarEvent request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Guid))
            {
                throw new CalendarEventDoesNotExistException(request.Guid);
            }

            await _repository.Delete(request.Guid);
        }

      
    }
}
