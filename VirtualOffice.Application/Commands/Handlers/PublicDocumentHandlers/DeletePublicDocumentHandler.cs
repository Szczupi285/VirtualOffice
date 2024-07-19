using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.PublicDocumentCommands;
using VirtualOffice.Application.Exceptions.PrivateDocument;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicDocumentHandlers
{
    public class DeletePublicDocumentHandler : IRequestHandler<DeletePublicDocument>
    {
        IPublicDocumentRepository _repository;
        IPublicDocumentReadService _readService;

        public DeletePublicDocumentHandler(IPublicDocumentRepository repository, IPublicDocumentReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(DeletePublicDocument request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PrivateDocumentDoesNotExistException(request.Id);

            await _repository.Delete(request.Id);
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
