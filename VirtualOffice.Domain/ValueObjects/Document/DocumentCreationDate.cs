using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Document;
using VirtualOffice.Domain.Exceptions.Subscription;

namespace VirtualOffice.Domain.ValueObjects.Document
{
    public sealed record DocumentCreationDate
    {
        public DateTime Value { get; }

        public DocumentCreationDate(DateTime value)
        {
            if (value > DateTime.UtcNow.AddMinutes(-1) && value < DateTime.UtcNow.AddMinutes(1))
                Value = value;

            throw new DocumentCreationDateCannotBeEitherPastOrFutureException(value);            
        }
    }
}
