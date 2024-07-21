using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Interfaces
{
    public interface IApplicationUser
    {
        ApplicationUserId Id { get; }
        ApplicationUserName _Name { get; }
        ApplicationUserSurname _Surname { get; }

        void EditName(ApplicationUserName name);
        void EditSurname(ApplicationUserSurname surname);
    }
}