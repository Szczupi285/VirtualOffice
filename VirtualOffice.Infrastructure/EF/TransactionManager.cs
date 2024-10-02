using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Infrastructure.EF
{
    public class TransactionManager : ITransactionManager
    {
        private readonly WriteDbContext _dbContext;

        public TransactionManager(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }
    }
}