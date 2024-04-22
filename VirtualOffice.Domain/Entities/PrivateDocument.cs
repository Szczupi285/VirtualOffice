using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Entities
{
    public class PrivateDocument : AbstractDocument
    {
        public DateTime _creationDate { get; private set; } = DateTime.Now;


        public void AddCreationDate(DateTime creationDate) => _creationDate = creationDate;

    }
    
}
