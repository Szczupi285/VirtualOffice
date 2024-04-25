using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Document;
using VirtualOffice.Domain.ValueObjects.Document;
using VirtualOffice.Domain.ValueObjects.Note;
using Xunit;

namespace DomainUnitTests
{
    public class AbstractDocumentValueObjectsUnitTests
    {
        #region DocumentContent
        [Fact]
        public void ValidDocumentContent_ValidDocumentContentToStringConversionShouldEqual()
        {
            DocumentContent title = "example";
            string test = title;

            Assert.Equal(test, title);
        }
        [Fact]
        public void ValidDocumentContent_StringToValidDocumentContentConversionShouldEqual()
        {
            string test = "example";
            DocumentContent title = test;
            Assert.Equal(test, title);
        }
        [Fact]
        public void NullDocumentContent_ShouldThrowEmptyDocumentContentException()
        {
            Assert.Throws<EmptyDocumentContentException>(() => new DocumentContent(null));
        }
        [Fact]
        public void EmptyDocumentContent_ShouldThrowEmptyDocumentContentException()
        {
            Assert.Throws<EmptyDocumentContentException>(() => new DocumentContent(""));
        }
        [Fact]
        public void TooLongDocumentContent_ShouldThrowTooLongDocumentContentException()
        {
            string invalidString = new string('a', 100001);
            Assert.Throws<TooLongDocumentContentException>(
                () => new DocumentContent(invalidString));
        }
        [Fact]
        public void MaxCharactersDocumentContent_ShouldNotThrowException()
        {
            string validString = new string('a', 100000);
            new DocumentContent(validString);
        }
        [Fact]
        public void MinCharactersDocumentContent_ShouldNotThrowException()
        {
            string validString = new string('a', 1);
            new DocumentContent(validString);
        }
        #endregion
        #region DocumentCreationDate
        #endregion
    }
}
