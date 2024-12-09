using MediatR;

namespace VirtualOffice.Application.Commands.OrganizationCommands
{
    public record AddOrganizationOfficeUsers(Guid OrganizationId, Guid OfficeId, ICollection<Guid> UserIds) : IRequest;
}