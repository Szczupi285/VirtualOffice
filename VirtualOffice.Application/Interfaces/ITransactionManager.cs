namespace VirtualOffice.Application.Interfaces
{
    public interface ITransactionManager
    {
        public Task BeginTransactionAsync();

        public Task CommitTransactionAsync();

        public Task RollbackTransactionAsync();
    }
}