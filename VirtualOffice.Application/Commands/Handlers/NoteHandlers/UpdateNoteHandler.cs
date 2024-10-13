﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Commands.NoteCommands;
using VirtualOffice.Application.Exceptions.Note;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.NoteHandlers
{
    public class UpdateNoteHandler : IRequestHandler<UpdateNote>
    {
        public INoteRepository _repository;
        public INoteReadService _readService;
        private const int _maxRetryAttempts = 3;
        private int _retryCount = 0;

        public UpdateNoteHandler(INoteRepository repository, INoteReadService noteReadService)
        {
            _repository = repository;
            _readService = noteReadService;
        }

        public async Task Handle(UpdateNote request, CancellationToken cancellationToken)
        {
            while (_retryCount < _maxRetryAttempts)
            {
                try
                {
                    var (Id, Title, Content) = request;

                    if (!await _readService.ExistsByIdAsync(Id))
                        throw new NoteDoesNoteExistsException(Id);

                    var note = await _repository.GetByIdAsync(Id);

                    if (note._title != Title)
                        note.EditTitle(Title);
                    if (note._content != Content)
                        note.EditContent(Content);

                    await _repository.UpdateAsync(note);

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