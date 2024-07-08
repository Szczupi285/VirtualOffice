using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Application.Exceptions.Meeting;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Application.Commands.Handlers.MeetingHandlers
{
    public class UpdateMeetingHandler : IRequestHandler<UpdateMeeting>
    {
        public IMeetingRepository _repository;
        public IMeetingEventReadService _readService;

        public UpdateMeetingHandler(IMeetingRepository repository, IMeetingEventReadService eventReadService)
        {
            _repository = repository;
            _readService = eventReadService;
        }

        public async Task HandleAsync(UpdateMeeting command, CancellationToken cancellationToken)
        {
            var (id, title, description, startDate, endDate) = command;

            if (await _readService.ExistsByIdAsync(id))
            {
                throw new MeetingDoesNotExistException(id);
            }

            var meeting = _repository.GetById(id);

            if(meeting._Title != title)
                meeting.SetTitle(title);
            if(meeting._Description != description)
                meeting.SetDescription(description);
            if(meeting._StartDate != startDate)
                meeting.UpdateStartDate(startDate);
            if(meeting._EndDate != endDate)
                meeting.UpdateEndDate(endDate);

            await _repository.Update(meeting);
        }
    }
}
