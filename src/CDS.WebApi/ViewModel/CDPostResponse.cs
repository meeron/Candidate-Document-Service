using CDS.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.WebApi.ViewModel
{
    public class CDPostResponse
    {
        private CDPostResponse()
        {
        }

        public bool IsSuccess { get; set; }

        public string DocumentId { get; set; }

        public Dictionary<string, string> Errors { get; set; }

        public static CDPostResponse Create(CandidateDocument doc)
        {
            return new CDPostResponse
            {
                IsSuccess = true,
                DocumentId = doc.Id.ToString()
            };
        }

        public static CDPostResponse Create(ModelStateDictionary modelState)
        {
            return new CDPostResponse
            {
                Errors = modelState.Where(e => e.Value.Errors.Count > 0).ToDictionary(k => k.Key, v => v.Value.Errors.First().ErrorMessage)
            };
        }
    }
}
