using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Application.DTO.ApplicationUser;
using VirtualOffice.Application.DTO.Office;
using VirtualOffice.Application.DTO.Organization;

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
        public async Task CreateOrganization([FromBody] CreateOrganizationRequest request)
        {
            var command = new CreateOrganization
                (
                    request.organizationName,
                    request.name,
                    request.surname
                );
            await _mediator.Send(command);
            Created();
        }

        [HttpDelete]
        public IActionResult DeleteOrganization(Guid Id)
        {
            return Ok();
        }

        [HttpPatch("{Id}/Name")]
        public IActionResult UpdateOrganizationName(Guid Id, string Name)
        {
            return Ok();
        }

        [HttpPost("{Id}/Employees")]
        public IActionResult AddOrganizationEmployees(Guid Id, ICollection<Guid> Users)
        {
            return Ok();
        }

        [HttpDelete("{Id}/Employees")]
        public IActionResult RemoveOrganizationEmployees(Guid Id, ICollection<Guid> Users)
        {
            return Ok();
        }

        [HttpPost("{OrganizationId}/{OfficeId}/Employees")]
        public IActionResult AddOfficeEmployees(Guid OrganizationId, Guid OfficeId, ICollection<Guid> Users)
        {
            return Ok();
        }

        [HttpDelete("{OrganizationId}/{OfficeId}/Employees")]
        public IActionResult RemoveOfficeEmployees(Guid OrganizationId, Guid OfficeId, ICollection<Guid> Users)
        {
            return Ok();
        }

        [HttpPost("{Id}/Office")]
        public IActionResult AddOffice(Guid Id, string Name, string Description, HashSet<Guid> Members)
        {
            return Ok();
        }

        [HttpDelete("{Id}/Office")]
        public IActionResult RemoveOffice(Guid Id, string Name, string Description, HashSet<Guid> Members)
        {
            return Ok();
        }

        [HttpGet("{Id}/Offices")]
        public ActionResult<OfficeIdAndNameDTO> GetOrganizationOffices(Guid Id)
        {
            var list = new List<OfficeIdAndNameDTO>()
            {
                new OfficeIdAndNameDTO()
                {
                    Id = Guid.NewGuid(),
                    _Name = "name1"
                },
                new OfficeIdAndNameDTO()
                {
                    Id = Guid.NewGuid(),
                    _Name = "name2"
                }
            };

            return Ok(list);
        }

        [HttpGet("{Id}/Users")]
        public ActionResult<ApplicationUserDTO> GetOrganizationUsers(Guid Id)
        {
            var list = new List<ApplicationUserDTO>()
            {
                new ApplicationUserDTO()
                {
                    Id = Guid.NewGuid(),
                    _Name = "name",
                    _Surname = "surname"
                },
                new ApplicationUserDTO()
                {
                    Id = Guid.NewGuid(),
                    _Name = "name1",
                    _Surname = "surname1"
                }
            };
            return Ok(list);
        }
    }
}