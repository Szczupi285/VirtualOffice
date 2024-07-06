﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.Handlers
{
    public class UpdateCalendarEventHandler : ICommandHandler<UpdateCalendarEvent>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;

        public UpdateCalendarEventHandler(ICalendarEventRepository repository, ICalendarEventReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task HandleAsync(UpdateCalendarEvent command, CancellationToken cancellationToken)
        {

            var (id, title, eventDescription, startDate, endDate) = command;

            if (!await _readService.ExistsByIdAsync(id))
            {
                throw new CalendarEventDoesNotExistException(id);
            }

            var calEv = await _repository.GetById(id);

            if(calEv._Title != title)
                calEv.SetTitle(title);
            if(calEv._Description != eventDescription)
                calEv.SetDescription(eventDescription);
            if(calEv._StartDate != startDate)
                calEv.UpdateStartDate(startDate);
            if(calEv._EndDate != endDate)
                calEv.UpdateEndDate(endDate);
                
            await _repository.Update(calEv);
        }
    }
}
