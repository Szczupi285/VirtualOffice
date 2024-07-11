using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.PrivateDocumentCommands;
using VirtualOffice.Application.Exceptions.PrivateDocument;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PrivateDocumentHandler
{
    public class DeletePrivateDocumentHandler : IRequestHandler<DeletePrivateDocument>
    {
        IPrivateDocumentRepository _repository;
        IPrivateDocumentReadService _readService;

        public DeletePrivateDocumentHandler(IPrivateDocumentRepository repository, IPrivateDocumentReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(DeletePrivateDocument request, CancellationToken cancellationToken)
        {

            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PrivateDocumentAlreadyDoeasNotExistException(request.Id);

            await _repository.Delete(request.Id);
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
