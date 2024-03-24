using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class InvalidOrganizationNameException : VirtualOfficeException
    {
        string Value; 
        public InvalidOrganizationNameException(string value) 
            : base($"Organizataion name: {value} is more than 100 characters long")
        {
            Value = value;
        }
    }
}
