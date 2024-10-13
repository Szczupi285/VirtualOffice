using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;
using VirtualOffice.Application.Exceptions.PublicChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicChatRoomHandlers
{
    public class UpdatePublicChatRoomNameHandler : IRequestHandler<UpdatePublicChatRoomName>
    {
        public IPublicChatRoomRepository _repository;
        public IPublicChatRoomReadService _readService;
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public UpdatePublicChatRoomNameHandler(IPublicChatRoomRepository repository, IPublicChatRoomReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdatePublicChatRoomName request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
                {
                    if (!await _readService.ExistsByIdAsync(request.Id))
                        throw new PublicChatRoomDoesNotExistException(request.Id);

                    var pcr = await _repository.GetByIdAsync(request.Id);

                    pcr.SetName(request.Name);

                    await _repository.UpdateAsync(pcr);

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