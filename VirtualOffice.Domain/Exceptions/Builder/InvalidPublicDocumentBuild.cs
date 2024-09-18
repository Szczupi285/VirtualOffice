using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.BuilderExceptions
{
    public class InvalidPublicDocumentBuild : VirtualOfficeException
    {
        public InvalidPublicDocumentBuild(bool isIdSet, bool isTitleSet, bool isContentSet, bool isCreationDetailsSet, bool isEligibleForReadSet, bool isEligibleForWriteSet)
            : base($"One of required components has not been set:\n" +
                  $"Id Set Status: {isIdSet}\n" +
                  $"Title Set Status: {isTitleSet}\n" +
                  $"Content Set Status: {isContentSet}\n" +
                  $"CreationDetails Set Status: {isCreationDetailsSet}\n" +
                  $"EligibleForRead Set Status: {isEligibleForReadSet}\n" +
                  $"EligibleForWrite Set Status: {isEligibleForWriteSet}\n")
        { }
    }
}