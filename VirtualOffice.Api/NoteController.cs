using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualOffice.Application.Commands.NoteCommands;
using VirtualOffice.Application.DTO.Note;

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
        public IActionResult DeleteNote([FromBody] Guid Id)
        {
            return Ok();
        }

        [HttpPatch("{Id}/Title")]
        public IActionResult UpdateNoteTitle([FromBody] Guid Id, string Title)
        {
            return Ok();
        }

        [HttpPatch("{Id}/Description")]
        public IActionResult UpdateNoteDescription([FromBody] Guid Id, string Title)
        {
            return Ok();
        }
    }
}