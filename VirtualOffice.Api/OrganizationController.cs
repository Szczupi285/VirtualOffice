using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Api
{
    [ApiController]
    [Route("api/Organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult CreateOrganization(string OrganizationName, Subscription Subscription, ApplicationUser User)
        {
            return Created();
        }

        [HttpDelete]
        public IActionResult DeleteOrganization([FromBody] Guid Id)
        {
            return Ok();
        }

        [HttpPatch("{Id}/Name")]
        public IActionResult UpdateOrganizationName(Guid Id, string Name)
        {
            return Ok();
        }

        [HttpPost("{Id}/Employees")]
        public IActionResult AddOrganizationEmployees(Guid Id, ICollection<ApplicationUser> Users)
        {
            return Ok();
        }

        [HttpDelete("{Id}/Employees")]
        public IActionResult RemoveOrganizationEmployees(Guid Id, ICollection<ApplicationUser> Users)
        {
            return Ok();
        }

        [HttpPost("{OrganizationId}/{OfficeId}/Employees")]
        public IActionResult AddOfficeEmployees(Guid OrganizationId, Guid OfficeId, ICollection<ApplicationUser> Users)
        {
            return Ok();
        }

        [HttpDelete("{OrganizationId}/{OfficeId}/Employees")]
        public IActionResult RemoveOfficeEmployees(Guid OrganizationId, Guid OfficeId, ICollection<ApplicationUser> Users)
        {
            return Ok();
        }

        [HttpPost("{Id}/Office")]
        public IActionResult AddOffice(Guid Id, string Name, string Description, HashSet<ApplicationUser> Members)
        {
            return Ok();
        }

        [HttpDelete("{Id}/Office")]
        public IActionResult RemoveOffice(Guid Id, string Name, string Description, HashSet<ApplicationUser> Members)
        {
            return Ok();
        }
    }
}