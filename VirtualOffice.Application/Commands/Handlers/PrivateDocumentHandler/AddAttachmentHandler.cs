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
    
    public class AddAttachmentHandler : IRequestHandler<AddAttachment>
    {
        IPrivateDocumentRepository _repository;
        IPrivateDocumentReadService _readService;

        public AddAttachmentHandler(IPrivateDocumentRepository repository, IPrivateDocumentReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(AddAttachment request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PrivateDocumentDoesNotExistException(request.Id);

            var privDoc = await _repository.GetById(request.Id);
            privDoc.AddNewAttachment(request.AttachmentFilePath);

            await _repository.Update(privDoc);
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
