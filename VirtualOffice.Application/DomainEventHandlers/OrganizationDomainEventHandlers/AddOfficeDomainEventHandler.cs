using AutoMapper;
using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.DomainEventHandlers.OrganizationDomainEventHandlers
{
    internal class AddOfficeDomainEventHandler
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        private readonly IMapper _mapper;

        public AddOfficeDomainEventHandler(IOutboxMessageRepository outboxMessageRepository, IMapper mapper)
        {
            _outboxMessageRepository = outboxMessageRepository;
            _mapper = mapper;
        }
    }
}