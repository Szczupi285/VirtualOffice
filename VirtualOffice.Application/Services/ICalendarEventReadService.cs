namespace VirtualOffice.Application.Services
{
    public interface ICalendarEventReadService
    {
        Task<bool> ExistsByIdAsync(Guid id);
    }
}
