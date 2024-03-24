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

       private OrganizationUsedSlots _UsedSlots;

        private Subscription subscription;

        private List<ApplicationUser> _organizationUsers;

        // private bool _isUnlimited 

        internal Organization()
        {
        }

    }
}
