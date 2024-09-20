using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.PublicDocumentCommands;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.Builders.Document;

namespace VirtualOffice.Application.Commands.Handlers.PublicDocumentHandlers
{
    public class AddPublicDocumentHandler : IRequestHandler<AddPublicDocument>
    {
        public IPublicDocumentRepository _repository;
        public IPublicDocumentReadService _readService;

        public AddPublicDocumentHandler(IPublicDocumentRepository repository, IPublicDocumentReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(AddPublicDocument request, CancellationToken cancellationToken)
        {
            PublicDocumentBuilder builder = new PublicDocumentBuilder();
            builder.SetId(Guid.NewGuid());
            builder.SetContent(request.content);
            builder.SetTitle(request.title);
            builder.SetCreationDetails(request.userId);
            builder.SetEligibleForRead(request.eligibleForRead);
            builder.SetEligibleForWrite(request.eligibleForWrite);

            var pubDoc = builder.GetDocument();

            await _repository.AddAsync(pubDoc);
        }
    }
}