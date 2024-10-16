using MediatR;
using VirtualOffice.Application.Commands.PublicDocumentCommands;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Builders.Document;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicDocumentHandlers
{
    internal sealed class AddPublicDocumentHandler : IRequestHandler<AddPublicDocument>
    {
        private readonly IPublicDocumentRepository _repository;
        private readonly IPublicDocumentReadService _readService;

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