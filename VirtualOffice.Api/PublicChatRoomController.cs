using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;
using VirtualOffice.Application.DTO.PublicChatRoom;
using VirtualOffice.Application.Models;

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

        [HttpGet("/user/{userId}")]
        public ActionResult<List<PublicChatRoomDTO>> GetPublicChatRoomsForUser(Guid userId)
        {
            var list = new List<PublicChatRoomDTO>()
            {
                new PublicChatRoomDTO()
                {
                    Id = Guid.NewGuid(),
                    _Name = "name1"
                },
                new PublicChatRoomDTO()
                {
                    Id = Guid.NewGuid(),
                    _Name = "name1"
                }
            };
            return Ok(list);
        }

        [HttpGet("{Id}")]
        public ActionResult<List<PublicChatRoomReadModel>> GetPrivateChatRoomById(Guid Id)
        {
            var users = new List<EmployeeReadModel>()
                    {
                        new EmployeeReadModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "name",
                            Surname = "surname",
                            Permissions = 0
                        },
                         new EmployeeReadModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "name1",
                            Surname = "surname1",
                            Permissions = 0
                        }
            };

            var list = new List<PublicChatRoomReadModel>()
            {
                new PublicChatRoomReadModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Users = users,
                    Messages = new List<MessageReadModel>
                    {
                        new MessageReadModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Content = "content",
                            Sender = users[0]
                        },
                         new MessageReadModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Content = "content1",
                            Sender = users[1]
                        }
                    }
                },
                new PublicChatRoomReadModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Users = users,
                    Messages = new List<MessageReadModel>
                    {
                        new MessageReadModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Content = "contentAnotherChat",
                            Sender = users[1]
                        },
                         new MessageReadModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Content = "contentAnotherChat",
                            Sender = users[1]
                        }
                    }
                },
            };
            return Ok(list);
        }
    }
}