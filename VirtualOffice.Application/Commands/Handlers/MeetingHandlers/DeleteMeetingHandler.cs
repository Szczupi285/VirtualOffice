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

namespace VirtualOffice.Application.Commands.Handlers.MeetingHandlers
{
    public class DeleteMeetingHandler
    {
        public IMeetingRepository _repository;
        public IMeetingEventReadService _readService;

        public DeleteMeetingHandler(IMeetingRepository repository, IMeetingEventReadService eventReadService)
        {
            _repository = repository;
            _readService = eventReadService;
        }

        public async Task HandleAsync(DeleteMeeting command, CancellationToken cancellationToken)
        {

            if (await _readService.ExistsByIdAsync(command.guid))
            {
                throw new MeetingDoesNotExistException(command.guid);
            }

            await _repository.Delete(command.guid);
        }
    }
}
