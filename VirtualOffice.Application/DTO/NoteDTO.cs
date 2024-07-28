using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Application.DTO
{
    public class NoteDTO
    {
        public Guid Id { get; init; }
        public Guid _userId { get; init; }
        public string _title { get; init; }
        public string _content { get; init; }

    }
}
