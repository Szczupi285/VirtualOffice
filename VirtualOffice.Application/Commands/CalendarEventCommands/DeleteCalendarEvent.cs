using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record DeleteCalendarEvent(Guid Id) : IRequest;
}