﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoom
{
    public class InvalidPrivateRoomParticipantsException : VirtualOfficeException
    {
        public InvalidPrivateRoomParticipantsException() : base($"Private Room must have exactly two participants")
        {
        }
    }
}
