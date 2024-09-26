namespace VirtualOffice.Application.Services
{
    public interface IPrivateChatRoomReadService
    {
        Task<bool> ExistsByIdAsync(Guid id);
    }
}
