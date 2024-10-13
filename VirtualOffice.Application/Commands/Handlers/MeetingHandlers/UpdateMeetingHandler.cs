using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public UpdateMeetingHandler(IMeetingRepository repository, IMeetingReadService eventReadService)
        {
            _repository = repository;
            _readService = eventReadService;
        }

        public async Task Handle(UpdateMeeting request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
                {
                    var (Id, Title, Description, StartDate, EndDate) = request;

                    if (!await _readService.ExistsByIdAsync(Id))
                        throw new MeetingDoesNotExistException(Id);

                    var meeting = await _repository.GetByIdAsync(Id);

                    if (meeting._Title != Title)
                        meeting.SetTitle(Title);
                    if (meeting._Description != Description)
                        meeting.SetDescription(Description);
                    if (meeting._StartDate != StartDate)
                        meeting.UpdateStartDate(StartDate);
                    if (meeting._EndDate != EndDate)
                        meeting.UpdateEndDate(EndDate);

                    await _repository.UpdateAsync(meeting);

                    break;
                }
                catch (DbUpdateConcurrencyException)
                {
                    _retryCount++;

                    // rethrowing exception after max attempts
                    if (_retryCount >= _maxRetryAttempts)
                        throw;

                    // each retry takes place 2x later than previous one e.g. 200ms => 400ms => 800ms
                    await Task.Delay(TimeSpan.FromMilliseconds(Math.Pow(2, _retryCount) * 100), cancellationToken);
                }
            }
        }
    }
}