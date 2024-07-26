﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Application.Commands.PublicDocumentCommands
{
    public record UpdatePublicDocumentContent(Guid Id, string Content) : IRequest;
}
