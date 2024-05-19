using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Event
{
    public class ThisEventIsAlreadyVIsibleToThisUser : VirtualOfficeException
    {
        Guid Value;
        public ThisEventIsAlreadyVIsibleToThisUser(Guid value) : base($"This event is already visible to user with Id: {value}")
        {
            Value = value;
        }
    }
}
