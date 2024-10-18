using Microsoft.Extensions.Options;
using VirtualOffice.Application.Models;
using VirtualOffice.Application.Models.ReadDatabaseSettings;
using VirtualOffice.Infrastructure.abstractions;

namespace VirtualOffice.Infrastructure.MongoDb.Services
{
    internal class EmployeesService : AbstractModelService<EmployeeReadModel>
    {
        public EmployeesService(IOptions<ReadDatabaseSettings> ReadDatabaseSettings)
            : base(ReadDatabaseSettings, ReadDatabaseSettings.Value.EmployeesCollectionName)
        {
        }
    }
}