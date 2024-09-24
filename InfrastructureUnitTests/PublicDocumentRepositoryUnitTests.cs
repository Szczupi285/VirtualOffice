using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Infrastructure.EF.Repositories;
using VirtualOffice.Infrastructure.EF;
using VirtualOffice.Domain.Builders.Document;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace InfrastructureUnitTests
{
    public class PublicDocumentRepositoryUnitTests
    {
        private readonly WriteDbContext _dbContext;
        private readonly PublicDocumentRepository _repository;
        private readonly Guid _pdGuid1;
        private readonly Guid _pdGuid2;
        private readonly Guid _pdGuid3;
        private readonly Guid _guid1;
        private readonly Guid _guid2;
        private readonly Guid _guid3;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly ApplicationUser _user3;
        private readonly List<PublicDocument> _data;
        private readonly PublicDocumentBuilder _builder;
        private readonly PublicDocument _doc;

        public PublicDocumentRepositoryUnitTests()
        {
            _pdGuid1 = Guid.NewGuid();
            _pdGuid2 = Guid.NewGuid();
            _pdGuid3 = Guid.NewGuid();

            _guid1 = Guid.NewGuid();
            _guid2 = Guid.NewGuid();
            _guid3 = Guid.NewGuid();
            _user1 = new(_guid1, "NameOne", "SurnameOne");
            _user2 = new(_guid2, "NameTwo", "SurnameTwo");
            _user3 = new(_guid3, "NameThree", "SurnameThree");

            _builder = new PublicDocumentBuilder();
            _builder.SetId(_pdGuid1);
            _builder.SetTitle("title");
            _builder.SetContent("content");
            _builder.SetCreationDetails(_guid1);
            _builder.SetEligibleForRead(new List<ApplicationUserId> { _guid1, _guid2, _guid3 });
            _builder.SetEligibleForWrite(new List<ApplicationUserId> { _guid1 });
            _doc = _builder.GetDocument();

            _data = new List<PublicDocument>
            {
                _doc
            };

            // setup in memory db
            var options = new DbContextOptionsBuilder<WriteDbContext>()
             .UseInMemoryDatabase(databaseName: "Test")
             .Options;

            _dbContext = new WriteDbContext(options);
            _repository = new PublicDocumentRepository(_dbContext);
            _dbContext.PublicDocuments.AddRange(_data[0]);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_pdGuid1);
            // Assert
            Assert.Equal(_data[0], result);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_EligibleForReadShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_pdGuid1);
            // Assert
            Assert.Equal(_data[0]._eligibleForRead, result._eligibleForRead);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_EligibleForWriteShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_pdGuid1);
            // Assert
            Assert.Equal(_data[0]._eligibleForRead, result._eligibleForRead);
        }

        [Fact]
        public async Task AddAsync_NewPublicDocument_ShouldContain()
        {
            // Arrange
            Guid tempGuid = Guid.NewGuid();
            _builder.SetId(tempGuid);
            _builder.SetTitle("title");
            _builder.SetContent("content");
            _builder.SetCreationDetails(_guid1);
            _builder.SetEligibleForRead(new List<ApplicationUserId> { _guid1, _guid2, _guid3 });
            _builder.SetEligibleForWrite(new List<ApplicationUserId> { _guid1 });
            var doc = _builder.GetDocument();
            // Act
            await _repository.AddAsync(doc);
            // Assert
            Assert.True(_dbContext.PublicDocuments.Contains(doc));
        }

        [Fact]
        public async Task DeleteAsync_PublicDocument_ShouldNotContain()
        {
            // Act
            await _repository.DeleteAsync(_data[0]);
            // Assert
            Assert.False(_dbContext.PublicDocuments.Contains(_data[0]));
        }

        [Fact]
        public async Task UpdateAsync_PublicDocument_ShouldNotContain()
        {
            // Arrange
            var document = await _repository.GetByIdAsync(_pdGuid1);
            document.AddEligibleForWrite(_guid2);
            // Act
            await _repository.UpdateAsync(document);
            // Assert
            Assert.True(_dbContext.PublicDocuments
                .First(x => x.Id.Value == _pdGuid1)._eligibleForWrite
                .Contains(_guid2));
        }
    }
}