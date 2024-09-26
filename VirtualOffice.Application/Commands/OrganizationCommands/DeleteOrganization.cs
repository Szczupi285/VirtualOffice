using MediatR;

namespace VirtualOffice.Application.Commands.OrganizationCommands
{
    public record DeleteOrganization(Guid Id) : IRequest;
}
