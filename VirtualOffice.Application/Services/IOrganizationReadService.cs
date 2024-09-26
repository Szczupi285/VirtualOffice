namespace VirtualOffice.Application.Services
{
    public interface IOrganizationReadService
    {
        Task<bool> ExistsByIdAsync(Guid id);
    }
}
