using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;

namespace VirtualOffice.Api
{
    [ApiController]
    [Route("api/PublicChatRoom")]
    public class PublicChatRoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PublicChatRoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult CreatePublicChatRoom([FromBody] CreatePublicChatRoom request)
        {
            return Created();
        }

        [HttpDelete]
        public IActionResult DeletePublicChatRoom(Guid Id)
        {
            return Ok();
        }

        [HttpPost("{ChatRoomId}/Message")]
        public IActionResult SendPublicMessage(Guid ChatRoomId, Guid UserId, string Content)
        {
            return Ok();
        }

        [HttpPost("{Id}/employees")]
        public IActionResult AddChatParticipants(Guid Id, HashSet<Guid> Participants)
        {
            return Ok();
        }

        [HttpDelete("{Id}/employees")]
        public IActionResult RemoveChatParticipants(Guid Id, HashSet<Guid> Participants)
        {
            return Ok();
        }

        [HttpPatch("{Id}/name")]
        public IActionResult UpdateName(Guid Id, string Name)
        {
            return Ok();
        }
    }
}