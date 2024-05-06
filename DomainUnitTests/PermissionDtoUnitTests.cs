using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Builders.Permission;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Dto;
using VirtualOffice.Domain.Services;

namespace DomainUnitTests
{
    public class PermissionDtoUnitTests
    {
        PermissionsEnum _Permissions;
        PermissionService _PermissionService;
        PermissionDto _PermissionDto;
        public PermissionDtoUnitTests()
        {
            PermissionBuilder builder = new PermissionBuilder();
            builder.SetCanAddEmployeeTask();
            builder.SetCanAddEventToOfficeCalendar();
            builder.SetCanAddEventToOrganizationCalendar();
            builder.SetCanAddToOffice();
            builder.SetCanCheckActivityLog();
            _Permissions = builder.GetPermissions();
            _PermissionService = new PermissionService(_Permissions);
            _PermissionDto = new PermissionDto(_PermissionService);
        }

        [Fact]
        public void PermissionDto_ListShouldContainCanAddEmployeeTask()
        {
            ICollection<string> list = _PermissionDto._Permissions;
            Assert.True(list.Contains("CanAddEmployeeTask"));
        }

        [Fact]
        public void PermissionDto_ListShouldContainCanAddEventToOfficeCalendar()
        {
            ICollection<string> list = _PermissionDto._Permissions;
            Assert.True(list.Contains("CanAddEventToOfficeCalendar"));
        }

        [Fact]
        public void PermissionDto_ListShouldContainCanAddEventToOrganizationCalendar()
        {
            ICollection<string> list = _PermissionDto._Permissions;
            Assert.True(list.Contains("CanAddEventToOrganizationCalendar"));
        }

        [Fact]
        public void PermissionDto_ListShouldContainCanAddToOffice()
        {
            ICollection<string> list = _PermissionDto._Permissions;
            Assert.True(list.Contains("CanAddToOffice"));
        }

        [Fact]
        public void PermissionDto_ListShouldContainCanCheckActivityLog()
        {
            ICollection<string> list = _PermissionDto._Permissions;
            Assert.True(list.Contains("CanCheckActivityLog"));
        }
        [Fact]
        public void PermissionDto_ListShouldHaveFiveElements()
        {
            ICollection<string> list = _PermissionDto._Permissions;
            Assert.True(list.Count == 5);
        }
        [Fact]
        public void PermissionDto_EmptyEnumInService_ListShouldContain_None()
        {
            PermissionService service = new PermissionService(new PermissionsEnum());
            ICollection<string> list = new PermissionDto(service)._Permissions;
            Assert.True(list.Contains("None"));
        }
        [Fact]
        public void PermissionDto_EmptyEnumInService_ListShouldHaveOneElement()
        {
            PermissionService service = new PermissionService(new PermissionsEnum());
            ICollection<string> list = new PermissionDto(service)._Permissions;
            Assert.Single(list);
        }


    }
}
