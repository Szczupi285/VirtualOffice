namespace VirtualOffice.Domain.DomainEvents.NoteEvents
{
    public record NoteDisabled(Guid Id) : IDomainEvent;
}