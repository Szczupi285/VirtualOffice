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
        IPublicDocumentRepository _repository;
        IPublicDocumentReadService _readService;

        public UpdatePublicDocumentTitleHandler(IPublicDocumentRepository repository, IPublicDocumentReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdatePublicDocumentTitle request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PublicDocumentDoesNotExistException(request.Id);

            var privDoc = await _repository.GetById(request.Id);
            privDoc.SetTitle(request.Title);

            await _repository.Update(privDoc);
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
