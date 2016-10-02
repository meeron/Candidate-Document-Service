using CDS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.Domain
{
    public interface IDbContext
    {
        ICollection<CandidateDocument> CandidateDocuments { get; }

        void DropDatabase();
    }
}
