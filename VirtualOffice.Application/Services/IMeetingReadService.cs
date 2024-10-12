namespace VirtualOffice.Application.Services
{
    public interface IMeetingReadService
    {
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
