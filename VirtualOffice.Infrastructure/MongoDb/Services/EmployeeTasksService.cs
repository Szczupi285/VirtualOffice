using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Infrastructure.abstractions;
using VirtualOffice.Infrastructure.EF.Models;
using VirtualOffice.Infrastructure.EF.Models.ReadDatabaseSettings;

namespace VirtualOffice.Infrastructure.MongoDb.Services
{
    public class EmployeeTaskService : AbstractModelService<EmployeeTaskReadModel>
    {
        public EmployeeTaskService(IOptions<ReadDatabaseSettings> ReadDatabaseSettings)
            : base(ReadDatabaseSettings, ReadDatabaseSettings.Value.EmployeeTasksCollectionName)
        {
        }
    }
}