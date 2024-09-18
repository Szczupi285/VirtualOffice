﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Infrastructure.EF.Models;
using VirtualOffice.Infrastructure.EF.Models.ReadDatabaseSettings;

namespace VirtualOffice.Infrastructure.MongoDb.Services
{
    public class EmployeesService : MongoDbService<UserReadModel>
    {
        public EmployeesService(IOptions<ReadDatabaseSettings> ReadDatabaseSettings)
            : base(ReadDatabaseSettings, ReadDatabaseSettings.Value.EmployeesCollectionName)
        {
        }
    }
}