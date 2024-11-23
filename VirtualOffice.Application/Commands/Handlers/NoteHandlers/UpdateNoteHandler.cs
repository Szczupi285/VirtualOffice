using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Commands.NoteCommands;
using VirtualOffice.Application.Exceptions.Note;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.NoteHandlers
{
    internal sealed class UpdateNoteHandler : IRequestHandler<UpdateNote>
    {
        private readonly INoteRepository _repository;
        private readonly INoteReadService _readService;
        private readonly IMediator _mediator;
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public UpdateNoteHandler(INoteRepository repository, INoteReadService noteReadService, IMediator mediator)
        {
            _repository = repository;
            _readService = noteReadService;
            _mediator = mediator;
        }

        public async Task Handle(UpdateNote request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
                {
                    var (Id, Title, Content) = request;
                    // if both valeus are null update doesn't make any sense so we throw InvalidOperationException();
                    // we don't use VirtualOffice exception since situation like this shouldn't happen and it
                    // occured by developer mistake in controller
                    if (Title is null && Content is null)
                        throw new InvalidOperationException("At least one value must be provided");

                    if (!await _readService.ExistsByIdAsync(Id))
                        throw new NoteDoesNoteExistsException(Id);

                    var note = await _repository.GetByIdAsync(Id);

                    // command accepts null values so we are able to update one property at the time
                    if (Title is not null && note._title != Title)
                        note.EditTitle(Title);
                    if (Content is not null && note._content != Content)
                        note.EditContent(Content);

                    await _repository.UpdateAsync(note);

                    foreach (var domainEvent in note.Events)
                        await _mediator.Publish(domainEvent, cancellationToken);
                    note.ClearEvents();

                    break;
                }
                catch (DbUpdateConcurrencyException)
                {
                    _retryCount++;

                    // rethrowing exception after max attempts
                    if (_retryCount >= _maxRetryAttempts)
                        throw;

                    // each retry takes place 2x later than previous one e.g. 200ms => 400ms => 800ms
                    await Task.Delay(TimeSpan.FromMilliseconds(Math.Pow(2, _retryCount) * 100), cancellationToken);
                }
            }
        }
    }
}