﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.PublicChatRoom;

namespace VirtualOffice.Application.Queries.PublicChatRoom
{
    public record GetPublicChatRoomById(Guid Id) : IRequest<PublicChatRoomDTO>;
}
