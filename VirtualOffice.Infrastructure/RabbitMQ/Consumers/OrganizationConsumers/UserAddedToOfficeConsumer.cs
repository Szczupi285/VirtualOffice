using MassTransit;
using VirtualOffice.Application.IntegrationEvents.OrganizationIntegrationEvent;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.OrganizationConsumers
{
    public class UserAddedToOfficeConsumer : IConsumer<UserAddedToOfficeIntegrationEvent>
    {
        private readonly OrganizationsService _organizationService;

        public UserAddedToOfficeConsumer(OrganizationsService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task Consume(ConsumeContext<UserAddedToOfficeIntegrationEvent> context)
        {
            await _organizationService.AddEmployeeToOffice(
                context.Message.OrganizationID, context.Message.OfficeId, context.Message.Employee);
        }
    }
}