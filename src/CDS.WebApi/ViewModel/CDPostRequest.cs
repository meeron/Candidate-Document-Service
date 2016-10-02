using CDS.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.WebApi.ViewModel
{
    public class CDPostRequest
    {
        [Required]
        public Guid? ApiKey { get; set; }

        [Required]
        public EDocumentType? Dt { get; set; } //DocumentId

        public Guid? EId { get; set; } //EmployerId
    }
}
