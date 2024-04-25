using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Document;

namespace VirtualOffice.Domain.ValueObjects.Document
{
    public sealed record DocumentFilePath : AbstractRecordName
    {
        public DocumentFilePath(string value) : base(value, 260, new EmptyDocumentFilePathException(value), new TooLongDocumentFilePathException(value, 260))
        {
            Regex regex = new Regex(@"^(?<ParentPath>(?:[a-zA-Z]\:|\\\\[\w\s\.]+\\[\w\s\.$]+)\\(?:[\w\s\.]+\\)*)(?<BaseName>[\w\s\.]*?)$");
            if (!regex.IsMatch(value))
                throw new InvalidDocumentFilePathException(value);
            
        }

        public static implicit operator DocumentFilePath(string content)
            => new(content);

    }
}
