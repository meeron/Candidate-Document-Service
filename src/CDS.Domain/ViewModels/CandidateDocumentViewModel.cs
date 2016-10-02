using CDS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.Domain.ViewModels
{
    public class CandidateDocumentViewModel
    {
        public Guid CandidateId { get; set; }

        public string FileName { get; set; }

        public string DocumentType { get; set; }

        public DateTime CreationDate { get; set; }

        public Guid? EmployerId { get; set; }

        public string FileExtension { get; set; }
    }
}
