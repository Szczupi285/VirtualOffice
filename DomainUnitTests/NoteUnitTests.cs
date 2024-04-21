using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.Exceptions.Note;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Note;

namespace DomainUnitTests
{
    public class NoteUnitTests
    {
        #region noteId
        [Fact]
        public void EmptyNoteId_ShouldReturnEmptyNoteIdException()
        {
            Assert.Throws<EmptyNoteIdException>(()
                => new NoteId(Guid.Empty));
        }
        [Fact]
        public void ValidNoteId_ValidGuidToNoteIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            NoteId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidNoteId_ValidNoteIdToGuidConversionShouldEqual()
        {

            NoteId id = new NoteId(Guid.NewGuid());

            Guid guid = id;
            Assert.Equal(id.Value, guid);

        }
        [Fact]
        public void ValidNoteId_GuidToValidNoteIdConversionShouldEqual()
        {

            NoteId id = new NoteId(Guid.NewGuid());

            Guid guid = id;

        }
        #endregion

        #region noteTitle
        [Fact]
        public void ValidNoteTitle_ValidNoteTitleToStringConversionShouldEqual()
        {
            NoteTitle title = "example";
            string test = title;

            Assert.Equal(test, title);
        }
        [Fact]
        public void ValidNoteTitle_StringToValidNoteTitleConversionShouldEqual()
        {
            string test = "example";
            NoteTitle title = test;
            Assert.Equal(test, title);
        }
        [Fact]
        public void NullNoteTitle_ShouldThrowEmptyNotTitleException()
        {
            Assert.Throws <EmptyNoteTitleException> (() => new NoteTitle(null));
        }
        [Fact]
        public void EmptyNoteTitle_ShouldThrowEmptyNotTitleException()
        {
            Assert.Throws<EmptyNoteTitleException>(() => new NoteTitle(""));
        }
        #endregion

        #region noteContent

        #endregion
    }
}
