using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Services
{
    public class PublicDocumentCaretaker
    {
        private readonly Stack<PublicDocumentMemento> _mementoStack = new();

        private readonly List<PublicDocumentMemento> _mementos = new List<PublicDocumentMemento>();
        private int _currentMementoIndex = -1;

        public void SaveState(PublicDocument document)
        {
            // If we're not at the latest state, remove all states after the current index
            if (_currentMementoIndex < _mementos.Count - 1)
            {
                _mementos.RemoveRange(_currentMementoIndex + 1, _mementos.Count - _currentMementoIndex - 1);
            }

            _mementos.Add(document.SaveToMemento());
            _currentMementoIndex++;
        }

        public bool RestorePreviousState(PublicDocument document)
        {
            if (_currentMementoIndex > 0)
            {
                _currentMementoIndex--;
                document.RestoreFromMemento(_mementos[_currentMementoIndex]);
                return true;
            }

            return false;
        }

        public bool RestoreNextState(PublicDocument document)
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
