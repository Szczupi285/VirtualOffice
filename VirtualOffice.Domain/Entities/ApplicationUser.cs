using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Entities
{
    public class ApplicationUser
    {
        public ApplicationUserId Id { get; private set; }

        public ApplicationUserName _name { get; private set; }

        public ApplicationUserSurname _surname { get; private set; }

        public ApplicationUserEmail _email { get; private set; }

        private PermissionsEnum _permissions;


        // private Settings _settings;

        // private Roles _roles;

        internal ApplicationUser(Guid id, string name, string surname, string email)
        {
            Id = id;
            _name = name;
            _surname = surname;
            _email = email;
            _permissions = PermissionsEnum.None;
        }

        internal ApplicationUser(Guid id, string name, string surname, PermissionsEnum permissions, string email) 
        {
            Id = id;
            _name = name;
            _surname = surname;
            _email = email;
            _permissions = permissions;
        }

    }
}
