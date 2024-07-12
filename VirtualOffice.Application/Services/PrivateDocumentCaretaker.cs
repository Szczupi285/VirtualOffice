using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Services
{
    public class PrivateDocumentCaretaker
    {
        private readonly Stack<PrivateDocumentMemento> _mementoStack = new();

        public void SaveState(PrivateDocument document)
        {
            _mementoStack.Push(document.SaveToMemento());
        }

        public void RestoreState(PrivateDocument document)
        {
            if (_mementoStack.Count > 0)
            {
                document.RestoreFromMemento(_mementoStack.Pop());
            }
        }
    }
}
