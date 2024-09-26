using VirtualOffice.Domain.Exceptions.Document;
using VirtualOffice.Domain.ValueObjects.Document;

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
        [Fact]
        public void DocumentCreationDate_PastDateWhileConstructing_ShouldThrowDocumentCreationDateCannotBeEitherPastOrFutureException()
        {
            Assert.Throws<DocumentCreationDateCannotBeEitherPastOrFutureException>(() => new DocumentCreationDate(DateTime.UtcNow.AddHours(-1)));
        }
        [Fact]
        public void DocumentCreationDate_FutureDateWhileConstructing_ShouldThrowDocumentCreationDateCannotBeEitherPastOrFutureException()
        {
            Assert.Throws<DocumentCreationDateCannotBeEitherPastOrFutureException>(() => new DocumentCreationDate(DateTime.UtcNow.AddHours(1)));
        }
        [Fact]
        public void ValidDocumentCreationDate_ShouldNotThrowException()
        {
            DocumentCreationDate DCD = DateTime.UtcNow;
        }
        #endregion
        #region DocumentFilePath
        [Fact]
        public void ValidDocumentFilePath_ValidDocumentFilePathToStringConversionShouldEqual()
        {
            DocumentFilePath filePath = @"C:\";
            string test = filePath;

            Assert.Equal(test, filePath);
        }
        [Fact]
        public void ValidDocumentFilePath_StringToValidDocumentFilePathConversionShouldEqual()
        {
            string test = @"C:\";
            DocumentFilePath filePath = test;
            Assert.Equal(test, filePath);
        }
        [Fact]
        public void NullDocumentFilePath_ShouldThrowEmptyDocumentFilePathException()
        {
            Assert.Throws<EmptyDocumentFilePathException>(() => new DocumentFilePath(null));
        }
        [Fact]
        public void EmptyDocumentFilePath_ShouldThrowEmptyDocumentFilePathException()
        {
            Assert.Throws<EmptyDocumentFilePathException>(() => new DocumentFilePath(""));
        }
        [Fact]
        public void TooLongDocumentFilePath_ShouldThrowTooLongFilePathException()
        {
            // 261 characters
            // this pattern match regex but exceeds max characters count
            string invalidString = @"C:\Users\Username\Documents\" + new string('a', 233);
            Assert.Throws<TooLongDocumentFilePathException>(
                () => new DocumentFilePath(invalidString));
        }
        [Fact]
        public void MaxCharactersDocumentFilePath_ShouldNotThrowException()
        {
            // 260 characters
            string validString = @"C:\Users\Username\Documents\" + new string('a', 232);
            new DocumentFilePath(validString);
        }
        [Fact]
        public void InvalidFilePath_Case1_ShouldThrowInvalidFilePathException()
        {
            string invalidString = @"fsa:\Users\Username\Documents\";
            Assert.Throws<InvalidDocumentFilePathException>(
                () => new DocumentFilePath(invalidString));
        }
        [Fact]
        public void InvalidFilePath_Case2_ShouldThrowInvalidFilePathException()
        {
            string invalidString = @"C:/Users/Username/Documents";
            Assert.Throws<InvalidDocumentFilePathException>(
                 () => new DocumentFilePath(invalidString));
        }
        [Fact]
        public void InvalidFilePath_Case3_ShouldThrowInvalidFilePathException()
        {
            string invalidString = @"C\Users\Username\Documents\";
            Assert.Throws<InvalidDocumentFilePathException>(
                () => new DocumentFilePath(invalidString));
        }
        [Fact]
        public void InvalidFilePath_Case5_ShouldThrowInvalidFilePathException()
        {
            string invalidString = @"C:*\Users\Username\Documents\";
            Assert.Throws<InvalidDocumentFilePathException>(
                 () => new DocumentFilePath(invalidString));
        }
        [Fact]
        public void InvalidFilePath_Case6_ShouldThrowInvalidFilePathException()
        {
            string invalidString = @"C:";
            Assert.Throws<InvalidDocumentFilePathException>(
                 () => new DocumentFilePath(invalidString));
        }

        #endregion

        #region DocumentId                      
        [Fact]
        public void EmptyDocumentId_ShouldThrowEmptyDocumentIdException()
        {
            Assert.Throws<EmptyDocumentIdException>(() => new DocumentId(Guid.Empty));
        }
        //---

        [Fact]
        public void ValidDocumentId_ValidGuidToDocumentIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();
            DocumentId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidDocumentId_ValidDocumentIdToGuidConversionShouldEqual()
        {
            DocumentId id = new DocumentId(Guid.NewGuid());
            Guid guid = id;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidDocumentId_GuidToValidDocumentIdConversionShouldEqual()
        {
            DocumentId id = new DocumentId(Guid.NewGuid());
            Guid guid = id;
        }
        #endregion

        #region DocumentTitle
        [Fact]
        public void ValidDocumentTitle_ValidDocumentTitleToStringConversionShouldEqual()
        {
            DocumentTitle title = "example title";
            string test = title;

            Assert.Equal(test, title);
        }
        [Fact]
        public void ValidDocumentTitle_StringToValidDocumentTitleConversionShouldEqual()
        {
            string test = "example title";
            DocumentTitle title = test;
            Assert.Equal(test, title);
        }
        [Fact]
        public void NullDocumentTitle_ShouldThrowEmptyDocumentTitleException()
        {
            Assert.Throws<EmptyDocumentTitleException>(() => new DocumentTitle(null));
        }
        [Fact]
        public void EmptyDocumentTitle_ShouldThrowEmptyDocumentTitleException()
        {
            Assert.Throws<EmptyDocumentTitleException>(() => new DocumentTitle(""));
        }
        [Fact]
        public void TooLongDocumentTitle_ShouldThrowTooLongDocumentTitleException()
        {
            string invalidString = new string('a', 51);
            Assert.Throws<TooLongDocumentTitleException>(
                () => new DocumentTitle(invalidString));
        }
        [Fact]
        public void MaxCharactersDocumentTitle_ShouldNotThrowException()
        {
            string validString = new string('a', 50);
            new DocumentTitle(validString);
        }
        [Fact]
        public void MinCharactersDocumentTitle_ShouldNotThrowException()
        {
            string validString = new string('a', 1);
            new DocumentTitle(validString);
        }
        #endregion
    }
}
