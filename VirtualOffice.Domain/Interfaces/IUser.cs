using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Interfaces
{
    public interface IUser
    {
        ApplicationUserName _Name { get; }
        ApplicationUserSurname _Surname { get; }
        ApplicationUserId Id { get; }
    }
}