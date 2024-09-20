using VirtualOffice.Domain.Builders.Permission;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Interfaces;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Entities
{
    public class ApplicationUser : IUser
    {
        public ApplicationUserId Id { get; }

        public ApplicationUserName _Name { get; private set; }

        public ApplicationUserSurname _Surname { get; private set; }

        public PermissionsEnum _Permissions { get; private set; }

        // private Settings _settings;

        // private Roles _roles;

        public ApplicationUser(Guid id, string name, string surname)
        {
            Id = id;
            _Name = name;
            _Surname = surname;
            _Permissions = PermissionsEnum.None;
        }

        public ApplicationUser(Guid id, string name, string surname, PermissionsEnum permissions)
        {
            Id = id;
            _Name = name;
            _Surname = surname;
            _Permissions = permissions;
        }

        private ApplicationUser()
        { }

        public void EditName(ApplicationUserName name) => _Name = name;

        public void EditSurname(ApplicationUserSurname surname) => _Surname = surname;

        public void SetAsEmployee() => _Permissions = PermissionDirector.ConstructEmployee();

        public void SetAsTeamLeader() => _Permissions = PermissionDirector.ConstructTeamLeader();

        public void SetAsManager() => _Permissions = PermissionDirector.ConstructManager();

        public void SetAsAdministrator() => _Permissions = PermissionDirector.ConstructAdministrator();

        public void SetAsMainAdministrator() => _Permissions = PermissionDirector.ConstuctMainAdministator();
    }
}