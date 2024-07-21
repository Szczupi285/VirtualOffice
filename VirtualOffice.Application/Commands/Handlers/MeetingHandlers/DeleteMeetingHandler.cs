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

namespace VirtualOffice.Application.Commands.Handlers.MeetingHandlers
{
    public class DeleteMeetingHandler : IRequestHandler<DeleteMeeting>
    {
        public IMeetingRepository _repository;
        public IMeetingReadService _readService;

        public DeleteMeetingHandler(IMeetingRepository repository, IMeetingReadService eventReadService)
        {
            _repository = repository;
            _readService = eventReadService;
        }

        public async Task Handle(DeleteMeeting request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Guid))
                throw new MeetingDoesNotExistException(request.Guid);

            await _repository.Delete(request.Guid);
            await _repository.SaveAsync(cancellationToken);

        }

    }
}
