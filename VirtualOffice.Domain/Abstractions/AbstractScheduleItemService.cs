using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.ChatRoomService;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;
using VirtualOffice.Shared;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract class AbstractScheduleItemService<T> where T : AbstractScheduleItem
    {
        protected internal HashSet<T> _ScheduleItems{ get; private set; }
        private protected readonly IDateTimeProvider _DateTimeProvider;

        protected AbstractScheduleItemService(HashSet<T> scheduleItems)
        {
            _ScheduleItems = scheduleItems;
            _DateTimeProvider = new DateTimeProvider();
        }
        internal protected AbstractScheduleItemService(HashSet<T> scheduleItems, IDateTimeProvider dateTimeProvider)
        {
            _ScheduleItems = scheduleItems;
            _DateTimeProvider = dateTimeProvider;
        }
        public bool AssignScheduleItem(T scheduleItem) => _ScheduleItems.Add(scheduleItem);

        public bool DeleteScheduleItem(T scheduleItem) => _ScheduleItems.Remove(scheduleItem);

        public T? GetScheduleItemById(ScheduleItemId id) => _ScheduleItems.FirstOrDefault(x => x.Id == id);

    }
}
