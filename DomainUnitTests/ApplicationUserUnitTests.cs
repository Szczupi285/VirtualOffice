using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace DomainUnitTests
{
    public class ApplicationUserUnitTests
    {
        #region ApplicationUserId

        [Fact]
        public void EmptyApplicationUserId_ShouldReturnEmptyApplicationUserIdException()
        {
            Assert.Throws<EmptyApplicationUserIdException>(()
                => new ApplicationUserId(Guid.Empty));
        }
        [Fact]
        public void ValidApplicationUserId_ValidGuidToAppUserIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            ApplicationUserId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidApplicationUserId_ValidAppUserIdToGuidConversionShouldEqual()
        {

            ApplicationUserId id = new ApplicationUserId(Guid.NewGuid());

            Guid guid = id;

            Assert.Equal(id.Value, guid);
        }
        #endregion

        #region ApplicationUserName

        [Fact]
        public void ValidApplicationUserName_ValidStringToAppUserNameConversion_ShouldEqual()
        {
            string name = "Michael";

            ApplicationUserName nam = name;

            Assert.Equal(name, nam);
        }
        [Fact]
        public void ValidApplicationUserName_ValidAppUserNameToStringConversionShouldEqual()
        {
            ApplicationUserName nam = new ApplicationUserName("Michael");

            string name = nam;

            Assert.Equal(nam, name);

        }

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
            ApplicationUserName value = "Thisusernameisvalid ";
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

        #region ApplicationUserSurname

        [Fact]
        public void EmtpyApplicationUserSurname_ShouldReturnEmptyApplicationUserSurnameException()
        {
            Assert.Throws<EmptyApplicationUserSurnameException>(()
                => new ApplicationUserSurname(""));
        }
        [Fact]
        public void NullApplicationUserSurname_ShouldReturnEmptyApplicationUserSurnameException()
        {
            Assert.Throws<EmptyApplicationUserSurnameException>(()
                => new ApplicationUserSurname(null));
        }
        [Theory]
        // 30char
        [InlineData("Thissurnameislongerthancharacterssss")]
        // 50 char
        [InlineData("ThissurnameislongerthancharactersThisusernameisllll")]
        public void TooLongApplicationUserSurname_ShouldReturnTooLongApplicationUserSurnameException(string surname)
        {
            Assert.Throws<TooLongApplicationUserSurnameException>(()
                => new ApplicationUserSurname(surname));
        }
        [Theory]
        [InlineData("Validsurname")]
        [InlineData("Anothersurname")]
        // 30char
        [InlineData("Thissurnameisexactlycharactetr")]
        public void ValidApplicationUserSurname_StringsShouldMatch(string surname)
        {
            ApplicationUserSurname sur = new ApplicationUserSurname(surname);

            Assert.Equal(surname, sur);
        }
        [Fact]
        public void ValidApplicationUserSurname_ValidStringToAppUserSurnameConversion_ShouldEqual()
        {
            string surname = "Examplesurname";

            ApplicationUserSurname sur = surname;

            Assert.Equal(surname, sur);
        }
        [Fact]
        public void ValidApplicationUserSurname_ValidAppUserSurnameTostringConversionShouldEqual()
        {
            ApplicationUserSurname sur = new ApplicationUserSurname("Examplesurname");

            string surname = sur;

            Assert.Equal(sur, surname);

        }

        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        [InlineData("4")]
        [InlineData("5")]
        [InlineData("6")]
        [InlineData("7")]
        [InlineData("8")]
        [InlineData("9")]
        [InlineData("!")]
        [InlineData("@")]
        [InlineData("#")]
        [InlineData("$")]
        [InlineData("%")]
        [InlineData("^")]
        [InlineData("&")]
        [InlineData("*")]
        [InlineData("(")]
        [InlineData(")")]
        [InlineData("-")]
        [InlineData("_")]
        [InlineData("+")]
        [InlineData("=")]
        [InlineData("{")]
        [InlineData("}")]
        [InlineData("[")]
        [InlineData("]")]
        [InlineData("|")]
        [InlineData("\\")]
        [InlineData(":")]
        [InlineData(";")]
        [InlineData("'")]
        [InlineData("<")]
        [InlineData(">")]
        [InlineData(",")]
        [InlineData(".")]
        [InlineData("?")]
        [InlineData("/")]
        [InlineData("`")]
        [InlineData("~")]
        public void InvalidApplicationUserSurnameShorterThan36char_ShouldReturnInvalidApplicationUserSurnameException(string surname)
        {
            Assert.Throws<InvalidApplicationUserSurnameException>(()
                => new ApplicationUserSurname(surname));
        }

        #endregion

    }

}