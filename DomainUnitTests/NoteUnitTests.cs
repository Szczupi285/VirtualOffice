﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.DomainEvents.NoteEvent;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.Exceptions.Note;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Message;
using VirtualOffice.Domain.ValueObjects.Note;

namespace DomainUnitTests
{
    public class NoteUnitTests
    {
        private Note _Note { get; set; }
        private ApplicationUserId UserId { get; set; }

        public NoteUnitTests()
        {
            UserId = Guid.NewGuid();    
            _Note = new Note(Guid.NewGuid(), "title", "content", UserId);
        }
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
        #endregion

        #region
        [Fact]
        public void EditContent_ShouldRaiseNoteContentChangedEvent()
        {
            _Note.EditContent("new");
            var Event = _Note.Events.OfType<NoteContentChanged>().Single();
        }
        [Fact]
        public void EditContent_ShouldRaiseNoteContentChangedEvent_NoteShouldEqual()
        {
            _Note.EditContent("new");
            var Event = _Note.Events.OfType<NoteContentChanged>().Single();
            Assert.Equal(_Note, Event.note);
        }
        [Fact]
        public void EditContent_ShouldRaiseNoteContentChangedEvent_ContentShouldEqual()
        {
            _Note.EditContent("new");
            var Event = _Note.Events.OfType<NoteContentChanged>().Single();
            Assert.Equal("new", Event.content);
        }
        [Fact]
        public void EditTitle_ShouldRaiseNoteTitleChangedEvent()
        {
            _Note.EditTitle("new");
            var Event = _Note.Events.OfType<NoteTitleChanged>().Single();
        }
        [Fact]
        public void EditTitle_ShouldRaiseNoteTitleChangedEvent_NoteShouldEqual()
        {
            _Note.EditTitle("new");
            var Event = _Note.Events.OfType<NoteTitleChanged>().Single();
            Assert.Equal(_Note, Event.note);
        }
        [Fact]
        public void EditTitle_ShouldRaiseNoteTitleChangedEvent_TitleShouldEqual()
        {
            _Note.EditTitle("new");
            var Event = _Note.Events.OfType<NoteTitleChanged>().Single();
            Assert.Equal("new", Event.title);
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
        public void NullNoteTitle_ShouldThrowEmptyNoteTitleException()
        {
            Assert.Throws <EmptyNoteTitleException> (() => new NoteTitle(null));
        }
        [Fact]
        public void EmptyNoteTitle_ShouldThrowEmptyNoteTitleException()
        {
            Assert.Throws<EmptyNoteTitleException>(() => new NoteTitle(""));
        }
        #endregion

        #region noteContent
        [Fact]
        public void ValidNoteContent_ValidNoteContentToStringConversionShouldEqual()
        {
            NoteContent title = "example";
            string test = title;

            Assert.Equal(test, title);
        }
        [Fact]
        public void ValidNoteContent_StringToValidNoteContentConversionShouldEqual()
        {
            string test = "example";
            NoteContent title = test;
            Assert.Equal(test, title);
        }
        [Fact]
        public void NullNoteContent_ShouldThrowEmptyNoteContentException()
        {
            Assert.Throws<EmptyNoteContentException>(() => new NoteContent(null));
        }
        [Fact]
        public void EmptyNoteContent_ShouldThrowEmptyNoteContentException()
        {
            Assert.Throws<EmptyNoteContentException>(() => new NoteContent(""));
        }
        [Fact]
        public void TooLongNoteContent_ShouldThrowTooLongNoteContentException()
        {
            string invalidString = new string('a', 1001);
            Assert.Throws<TooLongNoteContentException>(
                () => new NoteContent(invalidString));
        }
        [Fact]
        public void MaxCharactersNoteContent_ShouldNotThrowException()
        {
            string validString = new string('a', 1000);
            new NoteContent(validString);
        }
        [Fact]
        public void MinCharactersNoteContent_ShouldNotThrowException()
        {
            string validString = new string('a', 1);
            new NoteContent(validString);

        }
        #endregion

      
    }
}
