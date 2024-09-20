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
    public class UpdatePrivateDocumentTitleHandler : IRequestHandler<UpdatePrivateDocumentTitle>
    {
        private IPrivateDocumentRepository _repository;
        private IPrivateDocumentReadService _readService;

        public UpdatePrivateDocumentTitleHandler(IPrivateDocumentRepository repository, IPrivateDocumentReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdatePrivateDocumentTitle request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PrivateDocumentDoesNotExistException(request.Id);

            var privDoc = await _repository.GetById(request.Id);
            privDoc.SetTitle(request.Title);

            await _repository.UpdateAsync(privDoc);
        }
    }
}