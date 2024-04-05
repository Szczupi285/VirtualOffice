using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Office;
using VirtualOffice.Domain.ValueObjects.Office;
using VirtualOffice.Domain.ValueObjects.Subscription;

namespace DomainUnitTests
{
    public class OfficeUnitTests
    {
        Office _office;

        public OfficeUnitTests()
        {
            _office = new Office(Guid.NewGuid(), "Office", "Description", new List<ApplicationUser> { });
        }

        #region OfficeId
        [Fact]
        public void EmptyOfficeId_ShouldReturnEmptyOfficeIdException()
        {
            Assert.Throws<EmptyOfficeIdException>(()
                => new OfficeId(Guid.Empty));
        }

        [Fact]
        public void ValidOfficeId_ValidGuidToOfficeIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            OfficeId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidOfficeId_ValidOfficeIdToGuidConversion_ShouldEqual()
        {

            OfficeId id = new OfficeId(Guid.NewGuid());

            Guid guid = id;

            Assert.Equal(id.Value, guid);
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

        #region Adding Members

        [Fact]
        public void AddMember_ShouldContainMember()
        {
            ApplicationUser member = new ApplicationUser(Guid.NewGuid(), "name", "surname");
            _office.AddMember(member);
            
            Assert.Contains(member, _office._members);
        }

        [Fact]
        public void AddMember_AlreadyMember_ShouldThrowUserIsAlreadyMemberOfThisOfficeException()
        {
            ApplicationUser member = new ApplicationUser(Guid.NewGuid(), "name", "surname");
            _office.AddMember(member);

            Assert.Throws<UserIsAlreadyMemberOfThisOfficeException>(() => _office.AddMember(member));
        }
        [Fact]
        public void AddRangeMembers_ShouldContainMembers()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            ApplicationUser user1 = new ApplicationUser(guid1, "name", "surname");
            ApplicationUser user2 = new ApplicationUser(guid2, "name", "surname");
            ApplicationUser user3 = new ApplicationUser(guid3, "name", "surname");

            List<ApplicationUser> memberList = new List<ApplicationUser>
            {
                user1,
                user2,
                user3,
            };
            _office.AddRangeMembers(memberList);

            foreach(ApplicationUser member in memberList)
            {
                Assert.Contains(member, _office._members);
            }
        }

        #endregion

        #region Removing Members

        [Fact]
        public void RemoveMember_ShouldNotContainMember()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            ApplicationUser user1 = new ApplicationUser(guid1, "name", "surname");
            ApplicationUser user2 = new ApplicationUser(guid2, "name", "surname");
            ApplicationUser user3 = new ApplicationUser(guid3, "name", "surname");

            List<ApplicationUser> memberList = new List<ApplicationUser>
            {
                user1,
                user2,
                user3,
            };
            _office.AddRangeMembers(memberList);

            _office.RemoveMember(user1);

            Assert.DoesNotContain(user1, _office._members);
        }

        [Fact]
        public void RemoveMember_UserNotFound_ShouldThrowUserIsNotMemberOfThisOfficeException()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            ApplicationUser user1 = new ApplicationUser(guid1, "name", "surname");
            ApplicationUser user2 = new ApplicationUser(guid2, "name", "surname");
            ApplicationUser user3 = new ApplicationUser(guid3, "name", "surname");

            List<ApplicationUser> memberList = new List<ApplicationUser>
            {
                user1,
                user2,
                user3,
            };
            _office.AddRangeMembers(memberList);

            ApplicationUser user4 = new ApplicationUser(Guid.NewGuid(), "name", "surname");

            Assert.Throws<UserIsNotMemberOfThisOfficeException>(() => _office.RemoveMember(user4));
        }
        [Fact]
        public void RemoveMember_EmptyCollectionOfMembers_ShouldThrowUserIsNotMemberOfThisOfficeException()
        {

            ApplicationUser user4 = new ApplicationUser(Guid.NewGuid(), "name", "surname");

            Assert.Throws<UserIsNotMemberOfThisOfficeException>(() => _office.RemoveMember(user4));
        }

        [Fact]
        public void RemoveRangeMembers_ShouldNotContainMembers()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Guid guid4 = Guid.NewGuid();
            ApplicationUser user1 = new ApplicationUser(guid1, "name", "surname");
            ApplicationUser user2 = new ApplicationUser(guid2, "name", "surname");
            ApplicationUser user3 = new ApplicationUser(guid3, "name", "surname");
            ApplicationUser user4 = new ApplicationUser(guid4, "name", "surname");

            List<ApplicationUser> memberList = new List<ApplicationUser>
            {
                user1,
                user2,
                user3,
                user4
            };

            _office.AddRangeMembers(memberList);


            _office.RemoveRangeMembers(memberList);
            foreach (ApplicationUser member in memberList)
            {
                Assert.DoesNotContain(member, _office._members);
            }
        }

        #endregion

    }
}
