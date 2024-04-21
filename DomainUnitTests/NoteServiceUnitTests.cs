using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.NoteService;
using VirtualOffice.Domain.Services;
using VirtualOffice.Domain.ValueObjects.Note;

namespace DomainUnitTests
{
    public class NoteServiceUnitTests
    {
        private NoteService _NoteService { get; set; }
        private Guid _Id { get; set; } = Guid.NewGuid();

        public NoteServiceUnitTests()
        {
            NoteTitle title = new NoteTitle("title");
            NoteContent content = new NoteContent("content");

            Note note = new Note(_Id, title, content);
            ICollection<Note> notes = new List<Note>();
            notes.Add(note);
            _NoteService = new NoteService(notes);

        }

        [Fact]
        public void AddNote_ShouldAddNote()
        {
            NoteId id = new NoteId(Guid.NewGuid());
            NoteTitle title = new NoteTitle("title");
            NoteContent content = new NoteContent("content");

            Note note = new Note(id, title, content);

            _NoteService.AddNote(note);

            Assert.Equal(2, _NoteService.NotesCount);
        }
        [Fact]
        public void DeleteNote_ShouldRemoveNote()
        {
            NoteTitle title = new NoteTitle("title");
            NoteContent content = new NoteContent("content");

            Note note = new Note(_Id, title, content);

            _NoteService.DeleteNote(_Id);

            Assert.Equal(0, _NoteService.NotesCount);
        }

        [Fact]
        public void DeleteNote_NoteNotFound_ShouldReturnNoteNotFoundException()
        {
            NoteId id = new NoteId(Guid.NewGuid());
            Assert.Throws<NoteNotFoundException>(() => _NoteService.DeleteNote(id));
        }
        [Fact]
        public void EditNoteContent_ShouldUpdateNoteContent()
        {
            NoteContent content = new NoteContent("updatedContent");
            
            _NoteService.EditNoteContent(_Id, content);
            Note note = _NoteService.GetNoteById(_Id);
            Assert.Equal("updatedContent", note._content);
        }
        [Fact]
        public void EditNoteContent_NoteNotFound_ShouldReturnNoteNotFoundException()
        {
            NoteId id = new NoteId(Guid.NewGuid());
            NoteContent content = new NoteContent("content");

            Assert.Throws<NoteNotFoundException>(() => _NoteService.EditNoteContent(id, content));
        }
        [Fact]
        public void EditNoteTitle_ShouldUpdateNoteTitle()
        {
            NoteTitle title = new NoteTitle("updatedTitle");

            _NoteService.EditNoteTitle(_Id, title);
            Note note = _NoteService.GetNoteById(_Id);
            Assert.Equal("updatedTitle", note._title);
        }
        [Fact]
        public void EditNoteTitle_NoteNotFound_ShouldReturnNoteNotFoundException()
        {
            NoteId id = new NoteId(Guid.NewGuid());
            NoteTitle content = new NoteTitle("title");

            Assert.Throws<NoteNotFoundException>(() => _NoteService.EditNoteTitle(id, content));
        }
        [Fact]
        public void GetNoteById_NoteNotFound_ShouldReturnNoteNotFoundException()
        {
            NoteId id = new NoteId(Guid.NewGuid());
            Assert.Throws<NoteNotFoundException>(() => _NoteService.GetNoteById(id));
        }

    }
}
