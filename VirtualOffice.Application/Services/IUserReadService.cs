namespace VirtualOffice.Application.Services
{
    public interface IUserReadService
    {
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

