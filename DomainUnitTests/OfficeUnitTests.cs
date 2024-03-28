using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Office;
using VirtualOffice.Domain.ValueObjects.Office;

namespace DomainUnitTests
{
    public class OfficeUnitTests
    {
        #region OfficeId
        [Fact]
        public void EmptyOfficeId_ShouldReturnEmptyOfficeIdException()
        {
            Assert.Throws<EmptyOfficeIdException>(()
                => new OfficeId(Guid.Empty));
        }
        #endregion

        #region OfficeName
        [Fact]
        public void EmptyOfficeName_ShouldReturnEmptyOfficeNameException()
        {
            Assert.Throws<EmptyOfficeNameException>(()
                => new OfficeName(""));
        }

        [Fact]
        public void NullOfficeName_ShouldReturnEmptyOfficeNameException()
        {
            Assert.Throws<EmptyOfficeNameException>(()
                => new OfficeName(null));
        }

        [Theory]
        [InlineData("Thisofficenameislongerthan50charactersThisofficenameislonger")]
        public void InvalidOfficeName_ShouldReturnInvalidOfficeNameException(string input)
        {
            Assert.Throws<InvalidOfficeNameException>(()
                => new OfficeName(input));
        }

        [Theory]
        [InlineData("Thisofficenameisvalid")]
        [InlineData("Office")]
        [InlineData("O")]
        public void ValidOfficeName_StringShouldMatch(string input)
        {
            OfficeName value = input;
            Assert.Equal(input, value);
        }

        [Fact]
        public void WithLeadingWhitespacesOfficeName_StringShouldMatch()
        {
            OfficeName value = "  Thisofficenameisvalid";
            Assert.Equal("Thisofficenameisvalid", value);
        }

        [Fact]
        public void WithTrailingWhitespacesOfficeName_StringShouldMatch()
        {
            OfficeName value = "Thisofficenameisvalid ";
            Assert.Equal("Thisofficenameisvalid", value);
        }

        [Fact]
        public void WithTrailingAndLeadingWhitespacesOfficeName_StringShouldMatch()
        {

            OfficeName value = " Thisofficenameisvalid ";
            Assert.Equal("Thisofficenameisvalid", value);
        }

        [Fact]
        public void WithWhitespacesInBetweenOfficeName_StringShouldMatch()
        {
            OfficeName value = "Office office";
            Assert.Equal("Office office", value);
        }
        #endregion

        #region OfficeDescription
        [Fact]
        public void NullOfficeDescription_ShouldReturnOfficeDescriptionIsNullException()
        {
            Assert.Throws<OfficeDescriptionIsNullException>(()
                => new OfficeDescription(null));
        }

        [Theory]
        [InlineData("Thisusernameislongerthan200charactersThisusernameislongerthan200charactersThisusernameislongerthan200charactersThisusernameislongerthan200charactersThisusernameislongerthan200charactersThisusernameislongerthan200characters")]
        public void InvalidApplicationUserName_ShouldReturnInvalidOfficeDescriptionException(string input)
        {
            Assert.Throws<InvalidOfficeDescriptionException>(()
                => new OfficeDescription(input));
        }
        [Theory]
        [InlineData("Validofficedescription")]
        [InlineData("Description")]
        [InlineData("D")]
        public void ValidOfficeDescription_StringShouldMatch(string input)
        {
            OfficeDescription value = input;
            Assert.Equal(input, value);
        }
        #endregion

    }
}
