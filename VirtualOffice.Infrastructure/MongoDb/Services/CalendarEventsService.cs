using Microsoft.Extensions.Options;
using VirtualOffice.Infrastructure.abstractions;
using VirtualOffice.Application.Models.ReadDatabaseSettings;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Infrastructure.MongoDb.Services
{
    public class CalendarEventsService : AbstractModelService<CalendarEventReadModel>
    {
        public CalendarEventsService(IOptions<ReadDatabaseSettings> ReadDatabaseSettings)
            : base(ReadDatabaseSettings, ReadDatabaseSettings.Value.CalendarEventsCollectionName)
        {
        }
    }
}