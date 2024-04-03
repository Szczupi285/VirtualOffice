﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Office;
using VirtualOffice.Domain.Exceptions.Organization;
using VirtualOffice.Domain.ValueObjects.Office;
using VirtualOffice.Domain.ValueObjects.Organization;
using VirtualOffice.Domain.ValueObjects.Subscription;

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

        [Fact]
        public void ValidOrganizationId_ValidGuidToOrganizationIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            OrganizationId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidOrganizationId_ValidOrganizationIdToGuidConversion_ShouldEqual()
        {

            OrganizationId id = new OrganizationId(Guid.NewGuid());

            Guid guid = id;

            Assert.Equal(id.Value, guid);
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
