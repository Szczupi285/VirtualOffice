using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.NoteCommands
{
    public record CreateNote(string Title, string Content, ApplicationUser User) : IRequest;
}
