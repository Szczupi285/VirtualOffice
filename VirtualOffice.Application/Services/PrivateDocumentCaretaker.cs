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

        private readonly List<PrivateDocumentMemento> _mementos = new List<PrivateDocumentMemento>();
        private int _currentMementoIndex = -1;

        public void SaveState(PrivateDocument document)
        {
            // If we're not at the latest state, remove all states after the current index
            if (_currentMementoIndex < _mementos.Count - 1)
            {
                _mementos.RemoveRange(_currentMementoIndex + 1, _mementos.Count - _currentMementoIndex - 1);
            }

            _mementos.Add(document.SaveToMemento());
            _currentMementoIndex++;
        }

        public bool RestorePreviousState(PrivateDocument document)
        {
            if (_currentMementoIndex > 0)
            {
                _currentMementoIndex--;
                document.RestoreFromMemento(_mementos[_currentMementoIndex]);
                return true;
            }

            return false;
        }

        public bool RestoreNextState(PrivateDocument document)
        {
            if (_currentMementoIndex < _mementos.Count - 1)
            {
                _currentMementoIndex++;
                document.RestoreFromMemento(_mementos[_currentMementoIndex]);
                return true;
            }

            return false;
        }
    }
}
