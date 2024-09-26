using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.Document
{
    public record DocumentCreationDetails(DocumentCreationDate DocumentCreationDate, ApplicationUserId UserId)
    {
    }
}