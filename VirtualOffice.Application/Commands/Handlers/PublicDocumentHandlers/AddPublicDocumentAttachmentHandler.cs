using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.PrivateDocumentCommands;
using VirtualOffice.Application.Commands.PublicDocumentCommands;
using VirtualOffice.Application.Exceptions.PrivateDocument;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicDocumentHandlers
{
    public class AddPublicDocumentAttachmentHandler : IRequestHandler<AddPublicDocumentAttachment>
    {
        IPrivateDocumentRepository _repository;
        IPrivateDocumentReadService _readService;

        public AddPublicDocumentAttachmentHandler(IPrivateDocumentRepository repository, IPrivateDocumentReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(AddPublicDocumentAttachment request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PrivateDocumentDoesNotExistException(request.Id);

            var pubDoc = await _repository.GetById(request.Id);
            pubDoc.AddNewAttachment(request.AttachmentFilePath);

            await _repository.Update(pubDoc);
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
