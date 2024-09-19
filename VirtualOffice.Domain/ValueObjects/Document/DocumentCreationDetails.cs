using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.Document
{
    public record DocumentCreationDetails(DocumentCreationDate DocumentCreationDate, ApplicationUserId UserId)
    {
    }
}