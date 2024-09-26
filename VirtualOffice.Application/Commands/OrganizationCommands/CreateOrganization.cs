using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.OrganizationCommands
{
    public record CreateOrganization(string OrganizationName, Subscription Subscription, ApplicationUser User) : IRequest;
}
