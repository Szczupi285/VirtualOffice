using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Entities
{
    public class ApplicationUser
    {
        public ApplicationUserId Id { get; private set; }

        private ApplicationUserName _name;

        private ApplicationUserSurname _surname;

        // private Organization _organization; 

        // private IEnumerable<string> Offices;

        // private Permissions _permissions;

        // private Settings _settings;

        // private Roles _roles;

        internal ApplicationUser(Guid id, string name, string surname) 
        {
            Id = id;
            _name = name;
            _surname = surname;
        }

    }
}
