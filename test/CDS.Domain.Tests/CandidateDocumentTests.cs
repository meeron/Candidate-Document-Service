using CDS.Domain.Impl;
using CDS.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CDS.Domain.Tests
{
    public class CandidateDocumentTests: IDisposable
    {
        private readonly IDbContext _ctx;

        public CandidateDocumentTests()
        {
            //TODO: connection to test server in config
            _ctx = DbContextFactory.Create(string.Format("mongodb://localhost:27017/cds_unitest_{0}", Guid.NewGuid()));
        }

        [Fact]
        public void Add_Empty()
        {
            var ex = Assert.Throws<ValidationException>(() => _ctx.CandidateDocuments.Add(new CandidateDocument()));
            Assert.Equal("Field 'CandidateId' in 'CandidateDocument' has invalid value. Value is empty.", ex.Message);
        }

        [Fact]
        public void Add()
        {
            string testFile = "../test_cv.pdf";

            var doc = new CandidateDocument
            {
                CandidateId = Guid.NewGuid(),
                FileName = Path.GetFileName(testFile),
                DocumentType = Enums.EDocumentType.CV,
                CreationDate = DateTime.Now,
                Content = File.ReadAllBytes(testFile)         
            };
            _ctx.CandidateDocuments.Add(doc);

            Assert.Equal(".pdf", doc.FileExtension);
            Assert.NotNull(doc.Id);
        }

        [Fact]
        public void GetDocuments_By_CandidateId()
        {
            string testFile = "../test_cv.pdf";
            int count = 5;
            Guid candidateId = Guid.NewGuid();

            for (int i = 0; i < count; i++)
            {
                _ctx.CandidateDocuments.Add(new CandidateDocument
                {
                    CandidateId = candidateId,
                    FileName = Path.GetFileName(testFile),
                    DocumentType = Enums.EDocumentType.CV,
                    CreationDate = DateTime.Now,
                    Content = File.ReadAllBytes(testFile)
                });
            }

            var all = _ctx.CandidateDocuments.Where(x => x.CandidateId == candidateId).ToArray();

            Assert.Equal(count, all.Length);
        }

        public void Dispose()
        {
            _ctx.DropDatabase();
        }
    }
}
