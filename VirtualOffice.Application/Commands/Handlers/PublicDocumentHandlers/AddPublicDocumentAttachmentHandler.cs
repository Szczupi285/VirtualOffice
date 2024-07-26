using MediatR;
using VirtualOffice.Application.Commands.PublicDocumentCommands;
using VirtualOffice.Application.Exceptions.PublicDocument;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicDocumentHandlers
{
    public class AddPublicDocumentAttachmentHandler : IRequestHandler<AddPublicDocumentAttachment>
    {
        IPublicDocumentRepository _repository;
        IPublicDocumentReadService _readService;

        public AddPublicDocumentAttachmentHandler(IPublicDocumentRepository repository, IPublicDocumentReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(AddPublicDocumentAttachment request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PublicDocumentDoesNotExistException(request.Id);

            var pubDoc = await _repository.GetById(request.Id);
            pubDoc.AddNewAttachment(request.AttachmentFilePath);

            await _repository.Update(pubDoc);
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
