using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ApplicationUser
{
    public class ApplicationUserEmailCannotBeEmpty : VirtualOfficeException
    {
        public ApplicationUserEmailCannotBeEmpty() 
            : base("ApplicationUser E-Mail Cannot be empty")
        {
        }
    }
}
