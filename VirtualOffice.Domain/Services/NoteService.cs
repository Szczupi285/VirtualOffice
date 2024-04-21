using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.NoteService;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.Services
{
    public class NoteService
    {
        private ICollection<Note> _Notes { get; set; }
        public int NotesCount => _Notes.Count;

        public NoteService(ICollection<Note> notes)
        {
            _Notes = notes;
        }

        public ICollection<Note> GetAllNotes() => _Notes;

        public void AddNote(Note note) => _Notes.Add(note);
        public void DeleteNote(NoteId id)
        {
            Note note = _Notes.FirstOrDefault(n => n.Id == id) ?? throw new NoteNotFoundException(id);
            _Notes.Remove(note);
        }

        public void EditNoteContent(NoteId id, string content)
        {
            Note note = _Notes.FirstOrDefault(n => n.Id == id) ?? throw new NoteNotFoundException(id);
            note.EditContent(content);
        }
        public void EditNoteTitle(NoteId id, string title)
        {
            Note note = _Notes.FirstOrDefault(n => n.Id == id) ?? throw new NoteNotFoundException(id);
            note.EditTitle(title);
        }
        public Note GetNoteById(NoteId id) => _Notes.FirstOrDefault(n => n.Id == id) ?? throw new NoteNotFoundException(id);




    }
}
