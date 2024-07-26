using MediatR;
using VirtualOffice.Application.Commands.PublicDocumentCommands;
using VirtualOffice.Application.Exceptions.PublicDocument;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicDocumentHandlers
{
    public class DeletePublicDocumentAttachmentHandler : IRequestHandler<DeletePublicDocumentAttachment>
    {
        IPublicDocumentRepository _repository;
        IPublicDocumentReadService _readService;

        public DeletePublicDocumentAttachmentHandler(IPublicDocumentRepository repository, IPublicDocumentReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(DeletePublicDocumentAttachment request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PublicDocumentDoesNotExistException(request.Id);

            var pubDoc = await _repository.GetById(request.Id);
            pubDoc.DeleteAttachment(request.AttachmentFilePath);

            await _repository.Update(pubDoc);
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
