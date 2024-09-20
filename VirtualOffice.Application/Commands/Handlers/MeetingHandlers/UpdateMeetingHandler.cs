﻿using MediatR;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Application.Exceptions.Meeting;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.MeetingHandlers
{
    public class UpdateMeetingHandler : IRequestHandler<UpdateMeeting>
    {
        public IMeetingRepository _repository;
        public IMeetingReadService _readService;

        public UpdateMeetingHandler(IMeetingRepository repository, IMeetingReadService eventReadService)
        {
            _repository = repository;
            _readService = eventReadService;
        }

        public async Task Handle(UpdateMeeting request, CancellationToken cancellationToken)
        {
            var (Id, Title, Description, StartDate, EndDate) = request;

            if (!await _readService.ExistsByIdAsync(Id))
                throw new MeetingDoesNotExistException(Id);

            var meeting = await _repository.GetById(Id);

            if (meeting._Title != Title)
                meeting.SetTitle(Title);
            if (meeting._Description != Description)
                meeting.SetDescription(Description);
            if (meeting._StartDate != StartDate)
                meeting.UpdateStartDate(StartDate);
            if (meeting._EndDate != EndDate)
                meeting.UpdateEndDate(EndDate);

            await _repository.UpdateAsync(meeting);
        }
    }
}