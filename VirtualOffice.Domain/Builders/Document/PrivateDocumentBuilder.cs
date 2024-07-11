using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.BuilderExceptions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Builders.Document
{
    public class PrivateDocumentBuilder : IDocumentBuilder
    {
        private PrivateDocument _document = new PrivateDocument();

        private bool IsIdSet = false;
        private bool IsContentSet = false;
        private bool IsTitleSet = false;
        

        public PrivateDocumentBuilder()
        {
            Reset();
        }
        public void Reset() => _document = new PrivateDocument();

        public void SetAttachments(ICollection<DocumentFilePath> attachmentFilePaths)
        {
            _document.AddAttachment(attachmentFilePaths);
        }

        public void SetContent(string content)
        {
            _document.AddContent(content);
            IsContentSet = true;
        }

        public void SetId(Guid id)
        {
            _document.AddId(id);
            IsIdSet = true;
        }

        public void SetPreviousVersion(AbstractDocument previousVersion)
        {
            _document.AddPreviousVersion(previousVersion);
        }

        public void SetTitle(string title)
        {
            _document.AddTitle(title);
            IsTitleSet = true;
        }


        /// <summary>
        /// Builds a private document object based on the set properties. 
        /// </summary>
        /// <remarks>
        /// This method constructs a private document instance using the mandatory and optional properties that has been set previously.<br/>
        /// MANDATORY: <br/>
        /// <see cref="SetId(Guid)"/> <br/> 
        /// <see cref="SetTitle(string)"/> <br/>
        /// <see cref="SetContent(string)"/> <br/>
        /// OPTIONAL: <br/>
        /// <see cref="SetPreviousVersion(AbstractDocument)"/> <br/> 
        /// <see cref="SetAttachments(ICollection{DocumentFilePath})"/> <br/> 
        /// </remarks>
        /// <returns>
        /// A <see cref="PrivateDocument"/> object representing the constructed private document.
        /// </returns>
        /// <exception cref="InvalidPrivateDocumentBuild">
        /// Thrown when the build process fails due to missing or incomplete property values.
        /// </exception>
        public PrivateDocument GetDocument()
        {
            if (IsIdSet && IsTitleSet && IsContentSet)
            {
                PrivateDocument document = _document;
                Reset();
                return document;
            }
            else
            {
                Reset();
                throw new InvalidPrivateDocumentBuild(IsIdSet, IsTitleSet, IsContentSet);

            }

        }
    }
}
