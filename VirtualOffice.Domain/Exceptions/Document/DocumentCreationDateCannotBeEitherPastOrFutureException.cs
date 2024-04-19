using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class DocumentCreationDateCannotBeEitherPastOrFutureException : VirtualOfficeException
    {
        DateTime Value;
        public DocumentCreationDateCannotBeEitherPastOrFutureException(DateTime value) : base($"DocumentCreationDate: {value} cannot be either in the past or in the future")
        {
            Value = value;
        }
    }
}
