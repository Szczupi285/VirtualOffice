using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Api
{
    [ApiController]
    [Route("api/PrivateChatRoom")]
    public class PrivateChatRoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrivateChatRoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // private chat room will be created by sending 1st message to someone
        [HttpPost]
        public IActionResult CreatePrivateChatRoom(Guid SenderId, Guid ReciverId, [FromBody] Message Message)
        {
            return Created();
        }

        [HttpDelete]
        public IActionResult DeletePrivateChatRoom(Guid Id)
        {
            return Ok();
        }

        [HttpPost("{ChatRoomId}")]
        public IActionResult SendMessage(Guid ChatRoomId, Guid UserId, string Content)
        {
            return Ok();
        }
    }
}