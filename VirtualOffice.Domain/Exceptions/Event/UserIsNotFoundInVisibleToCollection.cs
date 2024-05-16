using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Event
{
    public class UserIsNotFoundInVisibleToCollection : VirtualOfficeException
    {
        Guid Id;

        public UserIsNotFoundInVisibleToCollection(Guid id) : base($"User with Id: {id} has not been found in VisibleTo collection")
        {
            Id = id;
        }
    }
}
