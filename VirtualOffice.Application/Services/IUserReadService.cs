﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Application.Services
{
    public interface IUserReadService
    {
        Task<bool> ExistsByIdAsync(Guid id);
    }
}

