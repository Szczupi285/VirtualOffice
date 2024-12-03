using MediatR;

namespace VirtualOffice.Application.Commands.OrganizationCommands
{
    public record CreateOrganization(string OrganizationName, Guid UserId) : IRequest;
}