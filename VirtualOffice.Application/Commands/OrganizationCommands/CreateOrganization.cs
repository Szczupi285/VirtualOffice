using MediatR;

namespace VirtualOffice.Application.Commands.OrganizationCommands
{
    public record CreateOrganization(string OrganizationName, string name, string surname) : IRequest;
}