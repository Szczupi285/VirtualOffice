using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Document;

namespace VirtualOffice.Domain.ValueObjects.Document
{
    public sealed record DocumentFilePath
    {
        public string Value { get; }
        public DocumentFilePath(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyDocumentFilePathException(value);
            }

            Value = value;
        }

        public static implicit operator DocumentFilePath(string content)
            => new(content);

    }
}
