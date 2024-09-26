namespace VirtualOffice.Application.Services
{
    public interface IEmployeeTaskReadService
    {
        Task<bool> ExistsByIdAsync(Guid id);
    }
}
