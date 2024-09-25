using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Infrastructure.EF.Repositories;
using VirtualOffice.Infrastructure.EF;

namespace InfrastructureUnitTests
{
    public class PublicChatRoomRepositoryUnitTests
    {
        private readonly WriteDbContext _dbContext;
        private readonly PublicChatRoomRepository _repository;
        private readonly Guid _pcrGuid1;
        private readonly Guid _pcrGuid2;
        private readonly Guid _pcrGuid3;
        private readonly Guid _guid1;
        private readonly Guid _guid2;
        private readonly Guid _guid3;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly ApplicationUser _user3;
        private readonly List<PublicChatRoom> _data;

        public PublicChatRoomRepositoryUnitTests()
        {
            _pcrGuid1 = Guid.NewGuid();
            _pcrGuid2 = Guid.NewGuid();
            _pcrGuid3 = Guid.NewGuid();

            _guid1 = Guid.NewGuid();
            _guid2 = Guid.NewGuid();
            _guid3 = Guid.NewGuid();
            _user1 = new(_guid1, "NameOne", "SurnameOne");
            _user2 = new(_guid2, "NameTwo", "SurnameTwo");
            _user3 = new(_guid3, "NameThree", "SurnameThree");
            Message message = new(Guid.NewGuid(), _user1, "TextMessage");

            _data = new List<PublicChatRoom>
            {
                new(_pcrGuid1, new HashSet<ApplicationUser>(){_user1, _user2}, new SortedSet<Message>{ }, "ChatRoom1"),
                new(_pcrGuid2, new HashSet<ApplicationUser>(){_user2, _user3}, new SortedSet<Message>{ }, "ChatRoom2"),
            };

            // setup in memory db
            var options = new DbContextOptionsBuilder<WriteDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            _dbContext = new WriteDbContext(options);
            _repository = new PublicChatRoomRepository(_dbContext);
            _dbContext.PublicChatRooms.AddRange(_data[0], _data[1]);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_pcrGuid1);
            // Assert
            Assert.Equal(_data[0], result);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ParticipantsShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_pcrGuid1);
            // Assert
            Assert.Equal(_data[0]._Participants, result._Participants);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_MessagesShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_pcrGuid1);
            // Assert
            Assert.Equal(_data[0]._Messages, result._Messages);
        }

        [Fact]
        public async Task AddAsync_NewPublicChatRoom_ShouldContain()
        {
            // Arrange
            Guid tempGuid = Guid.NewGuid();
            PublicChatRoom temp = new(tempGuid, new HashSet<ApplicationUser>() { _user1, _user3 }, new SortedSet<Message> { }, "NewChatRoom");
            // Act
            await _repository.AddAsync(temp);
            // Assert
            Assert.Contains(temp, _dbContext.PublicChatRooms);
        }

        [Fact]
        public async Task RemoveAsync_ExistingPublicChatRoom_ShouldNotContain()
        {
            // Act
            Assert.Contains(_data[0], _dbContext.PublicChatRooms);
            await _repository.DeleteAsync(_data[0]);
            // Assert
            Assert.DoesNotContain(_data[0], _dbContext.PublicChatRooms);
        }

        [Fact]
        public async Task UpdateAsync_ExistingPublicChatRoom_ShouldAddMessages()
        {
            // Act
            var pcr = await _repository.GetByIdAsync(_pcrGuid1);
            pcr.SendMessage(_user1, "NewMessage");
            await _repository.UpdateAsync(pcr);
            var result = _dbContext.PublicChatRooms.First(x => x.Id.Value == _pcrGuid1);
            // Assert
            Assert.Single(result._Messages);
        }
    }
}