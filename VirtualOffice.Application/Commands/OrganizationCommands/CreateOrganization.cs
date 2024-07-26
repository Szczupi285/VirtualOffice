﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.OrganizationCommands
{
    public record CreateOrganization(string OrganizationName, Subscription Subscription, ApplicationUser User) : IRequest;
}
