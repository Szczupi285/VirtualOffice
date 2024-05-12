using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Event
{
    internal class EventStartDateCannotBePastException : VirtualOfficeException
    {
        DateTime _startDate;
        public EventStartDateCannotBePastException(DateTime startDate) : base($"EventStartDate: '{startDate}' cannot be in the past.")
        {
            _startDate = startDate;
        }
    }
}
