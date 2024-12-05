using MediatR;

namespace VirtualOffice.Application.Commands.OrganizationCommands
{
    public record AddOffice(Guid OrganizationId, string Name, string Description, HashSet<Guid> Members) : IRequest;
}