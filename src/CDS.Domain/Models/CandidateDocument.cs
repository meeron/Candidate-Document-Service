using CDS.Domain.Enums;
using CDS.Domain.Impl;
using CDS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.Domain.Models
{
    public class CandidateDocument: ModelBase
    {
        public Guid CandidateId { get; set; }

        public string FileName { get; set; }

        public EDocumentType DocumentType { get; set; }

        public DateTime CreationDate { get; set; }

        public Guid? EmployerId { get; set; }

        public byte[] Content { get; set; }

        public string FileExtension { get; set; }

        public CandidateDocumentViewModel ToViewModel()
        {
            //TOOD: use AutoMapper

            return new CandidateDocumentViewModel
            {
                CandidateId = CandidateId,
                FileName = FileName,
                DocumentType = DocumentType.ToString(),
                CreationDate = CreationDate,
                EmployerId = EmployerId,
                FileExtension = FileExtension
            };
        }

        public override void ThrowIfInvalid()
        {
            //TODO: read properties attributes from System.ComponentModel.DataAnnotations (ex MaxLenght) for dynamic validation

            if (CandidateId.Equals(Guid.Empty))
                throw new ValidationException(this, "CandidateId", "Value is empty.");

            if (string.IsNullOrWhiteSpace(FileName))
                throw new ValidationException(this, "FileName", "Value is empty.");

            if (FileName.Length > 200)
                throw new ValidationException(this, "FileName", "Maximum value is 200.");

            if (string.IsNullOrWhiteSpace(FileExtension))
                throw new ValidationException(this, "FileExtension", "Value is empty.");

            if (FileExtension.Length > 5)
                throw new ValidationException(this, "FileExtension", "Maximum value is 5.");

            if (EmployerId.HasValue && EmployerId.Equals(Guid.Empty))
                throw new ValidationException(this, "EmployerId", "Value is empty.");

            if (Content == null)
                throw new ValidationException(this, "Content", "Value is empty.");

            if (Content.Length == 0)
                throw new ValidationException(this, "Content", "Value is empty.");
        }
    }
}
