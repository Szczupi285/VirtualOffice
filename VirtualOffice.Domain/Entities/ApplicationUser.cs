using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Entities
{
    public class ApplicationUser
    {
        public ApplicationUserId Id { get; private set; }

        public ApplicationUserName _name { get; private set; }

        public ApplicationUserSurname _surname { get; private set; }

        private Permissions _permissions;

        // private Settings _settings;

        // private Roles _roles;

        internal ApplicationUser(Guid id, string name, string surname, Permissions permissions) 
        {
            Id = id;
            _name = name;
            _surname = surname;
            _permissions = permissions;
        }

    }
}
