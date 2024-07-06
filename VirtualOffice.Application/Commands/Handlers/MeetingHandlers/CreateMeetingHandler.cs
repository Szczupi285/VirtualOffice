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
    public class CreateMeetingHandler : ICommandHandler<CreateMeeting>
    {
        public IMeetingRepository _repository;
        public IMeetingEventReadService _readService;

        public CreateMeetingHandler(IMeetingRepository repository, IMeetingEventReadService eventReadService)
        {
            _repository = repository;
            _readService = eventReadService;
        }

        public async Task HandleAsync(CreateMeeting command, CancellationToken cancellationToken)
        {
            var (id, title, description, assignedEmployees, startDate, endDate) = command;

            if (await _readService.ExistsByIdAsync(id))
            {
                throw new MeetingAlreadyExistsException(id);
            }

            Meeting meeting = new Meeting(id, title, description, assignedEmployees, startDate, endDate);

            await _repository.Add(meeting);
        }
    }
}
