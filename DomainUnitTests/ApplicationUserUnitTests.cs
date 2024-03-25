using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace DomainUnitTests
{
    public class ApplicationUserUnitTests
    {
        #region ApplicationUserName
        [Fact]
        public void EmptyApplicationUserName_ShouldReturnEmptyApplicationUserNameException()
        {
            Assert.Throws<EmptyApplicationUserNameException>(() 
                => new ApplicationUserName(""));
        }

        [Fact]
        public void NullApplicationUserName_ShouldReturnEmptyApplicationUserNameException()
        {
            Assert.Throws<EmptyApplicationUserNameException>(()
                => new ApplicationUserName(null));
        }
       

        [Theory]
        [InlineData("Thisusernameislongerthan30chara")]
        [InlineData("Thisusernameislongerthan35characters")]
        [InlineData("Thisusernameislongerthan50charactersThisusernameisl")]
        public void InvalidApplicationUserName_ShouldReturnTooLongApplicationUserNameException(string input)
        {
            Assert.Throws<TooLongApplicationUserNameException>(()
                => new ApplicationUserName(input));
        }
        [Theory]
        [InlineData("Thisusernameisvalid")]
        [InlineData("Michael")]
        [InlineData("Tom")]
        [InlineData("Bo")]
        public void ValidApplicationUserName_StringShouldMatch(string input)
        {
            ApplicationUserName value = input;
            Assert.Equal(input, value);
        }

        [Fact]
        public void WithLeadingWhitespacesApplicationUserName_StringShouldMatch()
        {
            ApplicationUserName value = " Thisusernameisvalid";
            Assert.Equal("Thisusernameisvalid", value);
        }
        [Fact]
        public void WithTrailingWhitespacesApplicationUserName_StringShouldMatch()
        {
            ApplicationUserName value = " Thisusernameisvalid";
            Assert.Equal("Thisusernameisvalid", value);
        }
        [Fact]
        public void WithTrailingAndLeadingWhitespacesApplicationUserName_StringShouldMatch()
        {
            
            ApplicationUserName value = " Thisusernameisvalid ";
            Assert.Equal("Thisusernameisvalid", value);
        }
        [Fact]
        public void WithWhitespacesInBetweenApplicationUserName_StringShouldMatch()
        {
            ApplicationUserName value = "Michael James";
            Assert.Equal("Michael James", value);
        }
        [Fact]
        public void WithDotInBetweenApplicationUserName_StringShouldMatch()
        {
            ApplicationUserName value = "Michael B.";
            Assert.Equal("Michael B.", value);
        }
        [Fact]
        public void WithDotAtTheEndApplicationUserName_StringShouldMatch()
        {
            ApplicationUserName value = "Michael .B";
            Assert.Equal("Michael .B", value);
        }

        [Fact]
        public void WithDotAtTheStartBetweenApplicationUserName_ShouldReturnInvalidApplicationUserNameException()
        {
            Assert.Throws<InvalidApplicationUserNameException>(()
                => new ApplicationUserName(".Michael"));
        }


        #endregion
    }

}