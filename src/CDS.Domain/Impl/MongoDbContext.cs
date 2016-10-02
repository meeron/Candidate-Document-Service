using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDS.Domain.Models;
using MongoDB.Driver;
using CDS.Domain.Impl;

namespace CDS.Domain.Impl
{
    public sealed class MongoDbContext : IDbContext
    {
        #region Variables
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly string _databaseName;
        #endregion

        #region Constructors
        public MongoDbContext(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");

            _databaseName = MongoUrl.Create(connectionString).DatabaseName;

            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(_databaseName);

            CandidateDocuments = new ModelCollection<CandidateDocument>(_database.GetCollection<CandidateDocument>("candidate_documents"));
        }
        #endregion

        #region Implementation of IDbContext
        public ICollection<CandidateDocument> CandidateDocuments { get; private set; }

        public void DropDatabase()
        {
            //prevent from accidently use of this method on production
#if DEBUG
            _client.DropDatabase(_databaseName);
#else
            throw new NotSupportedException();
#endif
        }
        #endregion
    }
}
