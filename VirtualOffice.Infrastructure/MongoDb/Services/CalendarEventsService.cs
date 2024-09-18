using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Infrastructure.EF.Models.ReadDatabaseSettings;
using VirtualOffice.Infrastructure.EF.Models;

namespace VirtualOffice.Infrastructure.MongoDb.Services
{
    public class CalendarEventsService : MongoDbService<CalendarEventReadModel>
    {
        public CalendarEventsService(IOptions<ReadDatabaseSettings> ReadDatabaseSettings)
            : base(ReadDatabaseSettings, ReadDatabaseSettings.Value.CalendarEventsCollectionName)
        {
        }
    }
}