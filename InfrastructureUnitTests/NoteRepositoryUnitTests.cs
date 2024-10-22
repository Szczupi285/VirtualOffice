using Microsoft.EntityFrameworkCore;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Infrastructure.EF;
using VirtualOffice.Infrastructure.EF.Repositories;

namespace InfrastructureUnitTests
{
    public class NoteRepositoryUnitTests
    {
        private readonly WriteDbContext _dbContext;
        private readonly NoteRepository _repository;
        private readonly Guid _noteGuid1;
        private readonly Guid _noteGuid2;
        private readonly Guid _noteGuid3;
        private readonly Guid _guid1;
        private readonly Guid _guid2;
        private readonly Guid _guid3;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly ApplicationUser _user3;
        private readonly List<Note> _data;

        public NoteRepositoryUnitTests()
        {
            _noteGuid1 = Guid.NewGuid();
            _noteGuid2 = Guid.NewGuid();
            _noteGuid3 = Guid.NewGuid();

            _guid1 = Guid.NewGuid();
            _guid2 = Guid.NewGuid();
            _guid3 = Guid.NewGuid();
            _user1 = new(_guid1, "NameOne", "SurnameOne");
            _user2 = new(_guid2, "NameTwo", "SurnameTwo");
            _user3 = new(_guid3, "NameThree", "SurnameThree");

            _data = new List<Note>
            {
                new(_noteGuid1, "title", "description", _user1),
                new(_noteGuid2, "title", "description", _user2),
                new(_noteGuid3, "title", "description", _user3),
            };

            // setup in memory db
            var options = new DbContextOptionsBuilder<WriteDbContext>()
             .UseInMemoryDatabase(databaseName: "Test")
             .Options;

            _dbContext = new WriteDbContext(options);
            _repository = new NoteRepository(_dbContext);
            _dbContext.Notes.AddRange(_data[0], _data[1], _data[2]);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_noteGuid1);
            // Assert
            Assert.Equal(_data[0], result);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_CreatedByShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_noteGuid1);
            // Assert
            Assert.Equal(_data[0]._createdBy, result._createdBy);
        }

        [Fact]
        public async Task AddAsync_NewNote_ShouldContain()
        {
            //Assert
            Guid testGuid = Guid.NewGuid();
            Note note = new(testGuid, "NewTitle", "NewContent", _user1);
            // Act
            await _repository.AddAsync(note);
            // Assert
            Assert.Contains(note, _dbContext.Notes);
        }

        [Fact]
        public async Task RemoveAsync_Note_ShouldNotContain()
        {
            // Act
            Assert.Contains(_data[0], _dbContext.Notes);
            await _repository.DeleteAsync(_data[0]);
            // Assert
            Assert.DoesNotContain(_data[0], _dbContext.Notes);
        }

        [Fact]
        public async Task UpdateAsync_Note_ShouldChangeTitle()
        {
            // Arrange
            var Note = await _repository.GetByIdAsync(_noteGuid1);
            string newTitle = "ChangedTitle";
            Note.EditTitle(newTitle);
            // Act
            await _repository.UpdateAsync(Note);
            var updatedNote = _dbContext.Notes.First(x => x.Id == Note.Id);
            // Assert
            Assert.Equal(newTitle, updatedNote._title);
        }

        [Fact]
        public async Task UpdateAsync_Note_ShouldChangeContent()
        {
            // Arrange
            var Note = await _repository.GetByIdAsync(_noteGuid1);
            string newDesc = "ChangedDesc";
            Note.EditContent(newDesc);
            // Act
            await _repository.UpdateAsync(Note);
            var updatedNote = _dbContext.Notes.First(x => x.Id == Note.Id);
            // Assert
            Assert.Equal(newDesc, updatedNote._content);
        }
    }
}