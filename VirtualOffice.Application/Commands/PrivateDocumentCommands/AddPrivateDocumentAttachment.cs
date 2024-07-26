﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Application.Commands.PrivateDocumentCommands
{
    public record AddPrivateDocumentAttachment(Guid Id, string AttachmentFilePath) : IRequest;
}
