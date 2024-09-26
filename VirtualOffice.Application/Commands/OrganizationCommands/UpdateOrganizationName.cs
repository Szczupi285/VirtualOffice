using MediatR;

namespace VirtualOffice.Application.Commands.OrganizationCommands
{
    public record UpdateOrganizationName(Guid OrganizationId, string Name) : IRequest;

}
