using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Services
{
    public class PublicDocumentCaretaker
    {
        private readonly Stack<PublicDocumentMemento> _mementoStack = new();

        public void SaveState(PublicDocument document)
        {
            _mementoStack.Push(document.SaveToMemento());
        }

        public void RestoreState(PublicDocument document)
        {
            if (_mementoStack.Count > 0)
            {
                document.RestoreFromMemento(_mementoStack.Pop());
            }
        }
    }
}
