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
using Microsoft.Extensions.DependencyInjection;

namespace VirtualOffice.Application.Commands.Handlers.PrivateDocumentHandler
{
    public class CreatePrivateDocumentHandler : IRequestHandler<CreatePrivateDocument>
    {
        private IPrivateDocumentRepository _repository;

        public CreatePrivateDocumentHandler(IPrivateDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreatePrivateDocument request, CancellationToken cancellationToken)
        {
            PrivateDocumentBuilder documentBuilder = new PrivateDocumentBuilder();

            documentBuilder.SetId(Guid.NewGuid());
            documentBuilder.SetContent(request.Content);
            documentBuilder.SetAttachments(request.FilePaths);
            documentBuilder.SetTitle(request.Title);

            await _repository.AddAsync(documentBuilder.GetDocument());
        }
    }
}