using CDS.Domain.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.Domain
{
    public static class DbContextFactory
    {
        public static IDbContext Create(string connectionString)
        {
            return new MongoDbContext(connectionString);
        }    
    }
}
