namespace VirtualOffice.Application.Services
{
    public interface IPublicChatRoomReadService
    {
        Task<bool> ExistsByIdAsync(Guid id);
    }
}
