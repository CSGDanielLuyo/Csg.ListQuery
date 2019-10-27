﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Csg.ListQuery;

namespace AspNetCoreWebAPI.Internal
{
    public class MockRepository : IRepository
    {
        public MockRepository()
        {
        }

        public Task<ListQueryResult<WeatherForecast>> GetWidgetsAsync(ListQueryDefinition queryDef)
        {
            throw new NotImplementedException();
        }
    }
}
