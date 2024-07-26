using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.NoteCommands
{
    public record UpdateNote(Guid Id, string Title, string Content) : IRequest;
}
