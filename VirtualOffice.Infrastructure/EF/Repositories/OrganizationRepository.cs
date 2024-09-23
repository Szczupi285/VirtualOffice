using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Infrastructure.EF.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly WriteDbContext _dbContext;

        public OrganizationRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Organization> GetByIdAsync(OrganizationId guid)
            => await _dbContext.Organizations
            .Include(o => o._organizationUsers)
            .Include(o => o._offices)
            .ThenInclude(oo => oo._members)
            .FirstOrDefaultAsync(o => o.Id == guid) ?? throw new OrganizationNotFoundException(guid);

        public async Task<Organization> GetByIdAsync(OrganizationId guid, CancellationToken cancellationToken)
             => await _dbContext.Organizations
            .Include(o => o._organizationUsers)
            .Include(o => o._offices)
            .ThenInclude(oo => oo._members)
            .FirstOrDefaultAsync(o => o.Id == guid, cancellationToken) ?? throw new OrganizationNotFoundException(guid);

        public async Task AddAsync(Organization organization)
        {
            await _dbContext.Organizations.AddAsync(organization);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(Organization organization, CancellationToken cancellationToken)
        {
            await _dbContext.Organizations.AddAsync(organization, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Organization organization)
        {
            _dbContext.Organizations.Remove(organization);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Organization organization, CancellationToken cancellationToken)
        {
            _dbContext.Organizations.Remove(organization);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Organization organization)
        {
            _dbContext.Organizations.Update(organization);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Organization organization, CancellationToken cancellationToken)
        {
            _dbContext.Organizations.Update(organization);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}