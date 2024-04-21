using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.SubscriptionService
{
    public class SubscriptionTypeCannotBeChangedToLowerTier : VirtualOfficeException
    {
        SubscriptionTypeEnum _CurrentSubscription;
        SubscriptionTypeEnum _NewSubscription;
        public SubscriptionTypeCannotBeChangedToLowerTier(SubscriptionTypeEnum currentSubscription, SubscriptionTypeEnum newSubscription) 
            : base($"Subcription Type cannot be changed to {newSubscription} from {currentSubscription} because it's lower or same tier")
        {
            _CurrentSubscription = currentSubscription;
            _NewSubscription = newSubscription;
        }
    }
}
