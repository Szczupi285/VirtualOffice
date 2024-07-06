using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Domain.Consts
{
    public enum SubscriptionTypeEnum
    {
        None = 0,
        Trial = 3,
        Basic = 30,
        Premium = 100,
        Enterprise = 500,
        Unlimited = -1,
    }
}
