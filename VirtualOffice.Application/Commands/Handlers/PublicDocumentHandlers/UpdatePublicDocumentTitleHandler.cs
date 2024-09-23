using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.PublicDocumentCommands;
using VirtualOffice.Application.Exceptions.PublicDocument;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicDocumentHandlers
{
    public class UpdatePublicDocumentTitleHandler : IRequestHandler<UpdatePublicDocumentTitle>
    {
        private IPublicDocumentRepository _repository;
        private IPublicDocumentReadService _readService;

        public UpdatePublicDocumentTitleHandler(IPublicDocumentRepository repository, IPublicDocumentReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdatePublicDocumentTitle request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PublicDocumentDoesNotExistException(request.Id);

            var pubDoc = await _repository.GetByIdAsync(request.Id);
            pubDoc.SetTitle(request.Title);

            await _repository.UpdateAsync(pubDoc);
        }
    }
}