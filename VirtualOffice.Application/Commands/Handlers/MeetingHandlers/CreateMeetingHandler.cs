using MediatR;
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

namespace VirtualOffice.Application.Commands.Handlers.MeetingEventHandlers
{
    public class CreateMeetingHandler : IRequestHandler<CreateMeeting>
    {
        public IMeetingRepository _repository;
        public IMeetingEventReadService _readService;

        public CreateMeetingHandler(IMeetingRepository repository, IMeetingEventReadService eventReadService)
        {
            _repository = repository;
            _readService = eventReadService;
        }

        public async Task Handle(CreateMeeting request, CancellationToken cancellationToken)
        {
            var (Id, Title, Description, AssignedEmployees, StartDate, EndDate) = request;

            if (await _readService.ExistsByIdAsync(Id))
            {
                throw new MeetingAlreadyExistsException(Id);
            }

            Meeting meeting = new Meeting(Id, Title, Description, AssignedEmployees, StartDate, EndDate);

            await _repository.Add(meeting);
        }

       
    }
}
