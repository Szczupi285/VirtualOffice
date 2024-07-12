using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.DomainEvents.ApplicationUserEvents;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.Abstractions;

namespace VirtualOffice.Domain.Entities
{
    public class ApplicationUser
    {
        public ApplicationUserId _Id { get; }

        public ApplicationUserName _Name { get; private set; }

        public ApplicationUserSurname _Surname { get; private set; }

        private PermissionsEnum _Permissions;

        // private Settings _settings;

        // private Roles _roles;

        public ApplicationUser(Guid id, string name, string surname)
        {
            _Id = id;
            _Name = name;
            _Surname = surname;
            _Permissions = PermissionsEnum.None;
        }

        public ApplicationUser(Guid id, string name, string surname, PermissionsEnum permissions) 
        {
            _Id = id;
            _Name = name;
            _Surname = surname;
            _Permissions = permissions;
        }

        public void EditName(ApplicationUserName name) => _Name = name;
        public void EditSurname(ApplicationUserSurname surname) => _Surname = surname;

    }
}
