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
    public class PrivateChatRoomRepositoryUnitTests
    {
        private readonly WriteDbContext _dbContext;
        private readonly PrivateChatRoomRepository _repository;
        private readonly Guid _pcrGuid1;
        private readonly Guid _pcrGuid2;
        private readonly Guid _pcrGuid3;
        private readonly Guid _guid1;
        private readonly Guid _guid2;
        private readonly Guid _guid3;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly ApplicationUser _user3;
        private readonly List<PrivateChatRoom> _data;

        public PrivateChatRoomRepositoryUnitTests()
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

            _data = new List<PrivateChatRoom>
            {
                new(_pcrGuid1, new HashSet<ApplicationUser>(){_user1, _user2}, new SortedSet<Message>{ }),
                new(_pcrGuid2, new HashSet<ApplicationUser>(){_user2, _user3}, new SortedSet<Message>{ }),
            };

            // setup in memory db
            var options = new DbContextOptionsBuilder<WriteDbContext>()
             .UseInMemoryDatabase(databaseName: "Test")
             .Options;

            _dbContext = new WriteDbContext(options);
            _repository = new PrivateChatRoomRepository(_dbContext);
            _dbContext.PrivateChatRooms.AddRange(_data[0], _data[1]);
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
        public async Task AddAsync_NewPrivateChatRoom_ShouldContain()
        {
            // Arrange
            Guid tempGuid = Guid.NewGuid();
            PrivateChatRoom temp = new(tempGuid, new HashSet<ApplicationUser>() { _user1, _user3 }, new SortedSet<Message> { });
            // Act
            await _repository.AddAsync(temp);
            // Assert
            Assert.Contains(temp, _dbContext.PrivateChatRooms);
        }

        [Fact]
        public async Task RemoveAsync_ExistingPrivateChatRoom_ShouldNotContain()
        {
            // Act
            Assert.Contains(_data[0], _dbContext.PrivateChatRooms);
            await _repository.DeleteAsync(_data[0]);
            // Assert
            Assert.DoesNotContain(_data[0], _dbContext.PrivateChatRooms);
        }

        [Fact]
        public async Task UpdateAsync_ExistingPrivateChatRoom_ShouldAddMessages()
        {
            // Act
            var pcr = await _repository.GetByIdAsync(_pcrGuid1);
            pcr.SendMessage(_user1, "NewMessage");
            await _repository.UpdateAsync(pcr);
            var result = await _dbContext.PrivateChatRooms.FirstAsync(x => x.Id.Value == _pcrGuid1);
            // Assert
            Assert.Single(result._Messages);
        }
    }
}