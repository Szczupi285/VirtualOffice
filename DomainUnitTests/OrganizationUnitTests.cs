using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Office;
using VirtualOffice.Domain.Exceptions.Organization;
using VirtualOffice.Domain.ValueObjects.Office;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace DomainUnitTests
{
    public class OrganizationUnitTests
    {
        #region OrganizationId
        [Fact]
        public void EmptyOrganizationId_ShouldReturnEmptyOrganizationIdException()
        {
            Assert.Throws<EmptyOrganizationIdException>(()
                => new OrganizationId(Guid.Empty));
        }
        #endregion

        #region OrganizationName
        [Fact]
        public void EmptyOrganizationName_ShouldReturnEmptyOrganizationNameException()
        {
            Assert.Throws<EmptyOrganizationNameException>(()
                => new OrganizationName(String.Empty));
        }

        [Theory]
        [InlineData("Thisorganizationnameislongerthan100charactersThisorganizationnameislongerthan100charactersThisorganization")]
        public void InvalidOrganizationName_ShouldReturnInvalidOrganizationNameException(string input)
        {
            Assert.Throws<InvalidOrganizationNameException>(()
                => new OrganizationName(input));
        }

        [Theory]
        [InlineData("Thisorganizationnameisvalid")]
        [InlineData("Organization")]
        [InlineData("O")]
        public void ValidOrganizationName_StringShouldMatch(string input)
        {
            OfficeName value = input;
            Assert.Equal(input, value);
        }
        #endregion

        #region OrganizationUserLimit
        [Theory]
        [InlineData(0)]
        [InlineData(1001)]
        public void InvalidOrganizationUserLimit_ShouldReturnInvalidOrganizationUserLimitException(ushort input)
        {
            Assert.Throws<InvalidOrganizationUserLimitException>(()
                => new OrganizationUserLimit(input));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(999)]
        public void ValidOrganizationUserLimit_NumberShouldMatch(ushort input)
        {
            OrganizationUserLimit value = (OrganizationUserLimit)input;
            Assert.Equal(input, value.Value);
        }
        #endregion

        #region OrganizationUsedSlots
        [Theory]
        [InlineData(0)]
        public void InvalidOrganizationUsedSlots_ShouldReturnInvalidOrganizationUsedSlotsException(uint input)
        {
            Assert.Throws<InvalidOrganizationUsedSlotsException>(()
                => new OrganizationUsedSlots(input));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1000)]
        public void ValidOrganizationUsedSlots_NumberShouldMatch(uint input)
        {
            OrganizationUserLimit value = (OrganizationUserLimit)input;
            Assert.Equal(input, value.Value);
        }
        #endregion
    }
}
