﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.SubscriptionService
{
    public class SubscriptionNotFoundException : VirtualOfficeException
    {
        Guid Value;
        public SubscriptionNotFoundException(Guid value) : base($"Subscription with Id: {value} has not been found")
        {
            Value = value;
        }
    }
}
