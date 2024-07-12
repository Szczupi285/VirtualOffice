using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.BuilderExceptions;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Builders.Document
{
    internal class PublicDocumentBuilder : IDocumentBuilder
    {
        private PublicDocument _document = new PublicDocument();

        private bool IsIdSet = false;
        private bool IsContentSet = false;
        private bool IsTitleSet = false;
        private bool IsCreationDetailsSet = false;
        private bool IsEligibleForReadSet = false;
        private bool IsEligibleForWriteSet = false;

        public PublicDocumentBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _document = new PublicDocument();
            IsIdSet = false;
            IsContentSet = false;
            IsTitleSet = false;
            IsCreationDetailsSet = false;
            IsEligibleForReadSet = false;
            IsEligibleForWriteSet = false;
        }

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

        public void SetTitle(string title)
        {
            _document.AddTitle(title);
            IsTitleSet = true;
        }

        public void SetCreationDetails(ApplicationUserId id)
        {
            _document.AddCreationDate(id);
            IsCreationDetailsSet = true;
        }

        public void SetEligibleForRead(ICollection<ApplicationUserId> eligibleForRead)
        {
            _document.AddEligibleForRead(eligibleForRead);
            IsEligibleForReadSet = true;
        }
        public void SetEligibleForWrite(ICollection<ApplicationUserId> eligibleForWrite)
        {
            _document.AddEligibleForWrite(eligibleForWrite);
            IsEligibleForWriteSet = true;
        }

        /// <summary>
        /// Builds a public document object based on the set properties. 
        /// </summary>
        /// <remarks>
        /// This method constructs a public document instance using the mandatory and optional properties that has been set previously.<br/>
        /// MANDATORY: <br/>
        /// <see cref="SetId(Guid)"/> <br/> 
        /// <see cref="SetTitle(string)"/> <br/>
        /// <see cref="SetContent(string)"/> <br/>
        /// <see cref="SetCreationDetails(ApplicationUserId)"/> <br/>
        /// <see cref="SetEligibleForRead(ICollection{ApplicationUserId})"/> <br/>
        /// <see cref="SetEligibleForWrite(ICollection{ApplicationUserId)"/> <br/>
        /// OPTIONAL: <br/>
        /// <see cref="SetPreviousVersion(AbstractDocument)"/> <br/> 
        /// <see cref="SetAttachments(ICollection{DocumentFilePath})"/> <br/> 
        /// </remarks>
        /// <returns>
        /// A <see cref="PublicDocument"/> object representing the constructed public document.
        /// </returns>
        /// <exception cref="InvalidPublicDocumentBuild">
        /// Thrown when the build process fails due to missing or incomplete property values.
        /// </exception>
        public PublicDocument GetDocument()
        {
            if(IsIdSet && IsTitleSet && IsContentSet
                && IsCreationDetailsSet && IsEligibleForReadSet
                && IsEligibleForWriteSet)
            {
                PublicDocument document = _document;
                Reset();
                return document;
            }
            else
            {
                Reset();
                throw new InvalidPublicDocumentBuild(IsIdSet, IsTitleSet, IsContentSet,
                    IsCreationDetailsSet, IsEligibleForReadSet, IsEligibleForWriteSet);
            }
            
        }
    }
}
