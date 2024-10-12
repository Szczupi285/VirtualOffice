namespace VirtualOffice.Application.Services
{
    public interface INoteReadService
    {
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
