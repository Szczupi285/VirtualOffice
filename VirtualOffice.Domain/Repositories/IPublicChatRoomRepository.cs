﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPublicChatRoomRepository
    {
        PublicChatRoom GetById(ChatRoomId guid);
        void Add(PublicChatRoom chatRoom);
        void Update(PublicChatRoom chatRoom);
        void Delete(ChatRoomId id);
        IEnumerable<PublicChatRoom> GetAllByUserId(ApplicationUserId id);
    }
}
