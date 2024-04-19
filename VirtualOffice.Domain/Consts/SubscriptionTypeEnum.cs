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
        Enterprise = 100,
        Premium = 500,
        Unlimited = -1,
    }
}
