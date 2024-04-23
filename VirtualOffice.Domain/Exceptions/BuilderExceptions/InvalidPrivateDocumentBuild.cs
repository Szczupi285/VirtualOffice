using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.BuilderExceptions
{
    public class InvalidPrivateDocumentBuild : VirtualOfficeException
    {
        public InvalidPrivateDocumentBuild(bool isIdSet, bool isTitleSet, bool isContentSet)
            : base($"One of required components has not been set:\n" +
                  $"Id Set Status: {isIdSet}\n" +
                  $"Title Set Status: {isTitleSet}\n" +
                  $"Content Set Status: {isContentSet}\n")
        { }
        
    }
}
