//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using VirtualOffice.Application.DTO.PrivateChatRoom;
//using VirtualOffice.Application.Models;
//using VirtualOffice.Domain.Entities;

//namespace VirtualOffice.Api
//{
//    [ApiController]
//    [Route("api/PrivateChatRoom")]
//    public class PrivateChatRoomController : ControllerBase
//    {
//        private readonly IMediator _mediator;

//        public PrivateChatRoomController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        // private chat room will be created by sending 1st message to someone
//        [HttpPost]
//        public IActionResult CreatePrivateChatRoom(Guid SenderId, Guid ReciverId, [FromBody] Message Message)
//        {
//            return Created();
//        }

//        [HttpDelete]
//        public IActionResult DeletePrivateChatRoom(Guid Id)
//        {
//            return Ok();
//        }

//        [HttpPost("{ChatRoomId}")]
//        public IActionResult SendMessage(Guid ChatRoomId, Guid UserId, string Content)
//        {
//            return Ok();
//        }

//        [HttpGet("/user/{userId}")]
//        public ActionResult<List<PrivateChatRoomDTO>> GetPrivateChatRoomsForUser(Guid userId)
//        {
//            var list = new List<PrivateChatRoomDTO>()
//            {
//                new PrivateChatRoomDTO()
//                {
//                    Id = Guid.NewGuid(),
//                    _ChatParticipantName = "name",
//                    _ChatParticipantSurname = "surname"
//                },
//                  new PrivateChatRoomDTO()
//                {
//                    Id = Guid.NewGuid(),
//                    _ChatParticipantName = "name1",
//                    _ChatParticipantSurname = "surname1"
//                }
//            };
//            return Ok(list);
//        }

//        [HttpGet("{Id}")]
//        public ActionResult<List<PrivateChatRoomReadModel>> GetPrivateChatRoomById(Guid Id)
//        {
//            var users = new List<EmployeeReadModel>()
//                    {
//                        new EmployeeReadModel()
//                        {
//                            Id = Guid.NewGuid().ToString(),
//                            Name = "name",
//                            Surname = "surname",
//                            Permissions = 0
//                        },
//                         new EmployeeReadModel()
//                        {
//                            Id = Guid.NewGuid().ToString(),
//                            Name = "name1",
//                            Surname = "surname1",
//                            Permissions = 0
//                        }
//            };

//            var list = new List<PrivateChatRoomReadModel>()
//            {
//                new PrivateChatRoomReadModel()
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Users = users,
//                    Messages = new List<MessageReadModel>
//                    {
//                        new MessageReadModel()
//                        {
//                            Id = Guid.NewGuid().ToString(),
//                            Content = "content",
//                            Sender = users[0]
//                        },
//                         new MessageReadModel()
//                        {
//                            Id = Guid.NewGuid().ToString(),
//                            Content = "content1",
//                            Sender = users[1]
//                        }
//                    }
//                },
//                new PrivateChatRoomReadModel()
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Users = users,
//                    Messages = new List<MessageReadModel>
//                    {
//                        new MessageReadModel()
//                        {
//                            Id = Guid.NewGuid().ToString(),
//                            Content = "contentAnotherChat",
//                            Sender = users[1]
//                        },
//                         new MessageReadModel()
//                        {
//                            Id = Guid.NewGuid().ToString(),
//                            Content = "contentAnotherChat",
//                            Sender = users[1]
//                        }
//                    }
//                },
//            };
//            return Ok(list);
//        }
//    }
//}