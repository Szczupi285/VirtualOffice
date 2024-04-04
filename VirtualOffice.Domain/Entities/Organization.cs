using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.Entities
{

    public class Organization
    {
        public OrganizationId Id { get; private set; }

        private OrganizationName _name;

        private OrganizationUserLimit _userLimit;

        private OrganizationUsedSlots _usedSlots;

        private ICollection<Office> _offices;

        // consider this relation since office already contains 
        // users list, but we would have to check distinct users every time
        // we add user to office. Since one user may be a member of many offices
        private ICollection<ApplicationUser> _organizationUsers;

        private Subscription subscription;

        private bool _isUnlimited;

        internal Organization()
        {
        }

    }
}
