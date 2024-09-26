namespace VirtualOffice.Application.Services
{
    public interface IPublicDocumentReadService
    {
        Task<bool> ExistsByIdAsync(Guid id);
    }
}
