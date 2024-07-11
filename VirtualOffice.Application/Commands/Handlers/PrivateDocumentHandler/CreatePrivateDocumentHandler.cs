using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.PrivateDocumentCommands;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.Builders.Document;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;
using VirtualOffice.Application.Exceptions.PrivateDocument;

namespace VirtualOffice.Application.Commands.Handlers.PrivateDocumentHandler
{
    public class CreatePrivateDocumentHandler : IRequestHandler<PrivateDocument>
    {
        IPrivateDocumentRepository _repository;
        IPrivateDocumentReadService _readService;

        public CreatePrivateDocumentHandler(IPrivateDocumentRepository repository, IPrivateDocumentReadService service)
        {
            _repository = repository;
            _readService = service;
        }

        public async Task Handle(PrivateDocument request, CancellationToken cancellationToken)
        {
            if (await _readService.ExistsByIdAsync(request.id))
                throw new PrivateDocumentAlreadyExistException(request.id);

            PrivateDocumentBuilder documentBuilder = new PrivateDocumentBuilder();
            Guid id = request.id;
            string content = request.content;
            string title = request.title;
            List<DocumentFilePath> attachmentFilePaths = new List<DocumentFilePath> {request.filePath};

            await _repository.Add(documentBuilder.GetDocument());
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
