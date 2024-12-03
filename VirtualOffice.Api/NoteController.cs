using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Application.Commands.NoteCommands;
using VirtualOffice.Application.DTO.Note;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Api
{
    [ApiController]
    [Route("api/Notes")]
    public class NoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task CreateNote([FromBody] NoteDTO request)
        {
            var command = new CreateNote(request.Title, request.Content, request.UserId);
            await _mediator.Send(command);
            Created();
        }

        [HttpDelete]
        public async Task DeleteNote([FromBody] Guid Id)
        {
            var command = new DeleteNote(Id);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("{Id}/Title")]
        public async Task UpdateNoteTitle(Guid Id, string Title)
        {
            var command = new UpdateNote(Id, Title, null);
            await _mediator.Send(command);
            Ok();
        }

        [HttpPatch("{Id}/Description")]
        public async Task UpdateNoteDescription(Guid Id, string Description)
        {
            var command = new UpdateNote(Id, null, Description);
            await _mediator.Send(command);
            Ok();
        }

        [HttpGet("{Id}")]
        public ActionResult<NoteReadModel> GetNoteById(Guid Id)
        {
            var note = new NoteReadModel
            {
                Id = Guid.NewGuid().ToString(),
                Title = "mock",
                Content = "MockContent",
                CreatedBy = new EmployeeReadModel { Id = Guid.NewGuid().ToString(), Name = "Alice Johnson" }
            };
            return Ok(note);
        }

        [HttpGet("/user/")]
        public ActionResult<IEnumerable<NoteTitleDTO>> GetNotesForUser([FromQuery] Guid Id)
        {
            var list = new List<NoteTitleDTO>()
            {
                new NoteTitleDTO
                {
                    Id = Guid.NewGuid(),
                    _Title = "mock1"
                },
                 new NoteTitleDTO
                {
                    Id = Guid.NewGuid(),
                    _Title = "mock2"
                },
            };
            return Ok(list);
        }

        [HttpGet("/title/user/{Id}")]
        public ActionResult<IEnumerable<NoteTitleDTO>> GetNotesForUserByTitle(Guid Id, string title)
        {
            var list = new List<NoteTitleDTO>()
            {
                new NoteTitleDTO
                {
                    Id = Guid.NewGuid(),
                    _Title = "mock1"
                },
                 new NoteTitleDTO
                {
                    Id = Guid.NewGuid(),
                    _Title = "mock2"
                },
            };
            return Ok(list);
        }
    }
}