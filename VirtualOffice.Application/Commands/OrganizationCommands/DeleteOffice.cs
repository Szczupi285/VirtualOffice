using MediatR;

namespace VirtualOffice.Application.Commands.OrganizationCommands
{
    public record DeleteOffice(Guid OrganizationId, Guid OfficeId) : IRequest;
}
