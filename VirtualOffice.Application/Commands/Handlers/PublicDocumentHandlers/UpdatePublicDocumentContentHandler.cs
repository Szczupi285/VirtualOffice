using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Commands.PublicDocumentCommands;
using VirtualOffice.Application.Exceptions.PublicDocument;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicDocumentHandlers
{
    public class UpdatePublicDocumentContentHandler : IRequestHandler<UpdatePublicDocumentContent>
    {
        private IPublicDocumentRepository _repository;
        private IPublicDocumentReadService _readService;
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public UpdatePublicDocumentContentHandler(IPublicDocumentRepository repository, IPublicDocumentReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdatePublicDocumentContent request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
                {
                    if (!await _readService.ExistsByIdAsync(request.Id))
                        throw new PublicDocumentDoesNotExistException(request.Id);

                    var pubDoc = await _repository.GetByIdAsync(request.Id);
                    pubDoc.SetContent(request.Content);

                    await _repository.UpdateAsync(pubDoc);

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