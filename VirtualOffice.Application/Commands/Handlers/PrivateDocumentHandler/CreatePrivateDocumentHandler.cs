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
using VirtualOffice.Domain.Entities;
using VirtualOffice.Application.Commands.CreatePrivateDocumentCommands;

namespace VirtualOffice.Application.Commands.Handlers.PrivateDocumentHandler
{
    public class CreatePrivateDocumentHandler : IRequestHandler<CreatePrivateDocument>
    {
        IPrivateDocumentRepository _repository;
        IPrivateDocumentReadService _readService;

        public CreatePrivateDocumentHandler(IPrivateDocumentRepository repository, IPrivateDocumentReadService service)
        {
            _repository = repository;
            _readService = service;
        }

        public async Task Handle(CreatePrivateDocument request, CancellationToken cancellationToken)
        {
            if (await _readService.ExistsByIdAsync(request.Id))
                throw new PrivateDocumentAlreadyExistException(request.Id);

            PrivateDocumentBuilder documentBuilder = new PrivateDocumentBuilder();
            Guid id = request.Id;
            string content = request.Content;
            string title = request.Title;
            List<DocumentFilePath> attachmentFilePaths = new List<DocumentFilePath> {request.FilePath};

            await _repository.Add(documentBuilder.GetDocument());
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
