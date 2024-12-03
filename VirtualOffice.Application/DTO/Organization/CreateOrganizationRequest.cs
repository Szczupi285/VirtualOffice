namespace VirtualOffice.Application.DTO.Organization
{
    public record CreateOrganizationRequest(string organizationName, Guid userId);
}