using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CDS.Domain.Models;
using CDS.Domain;
using CDS.WebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using CDS.WebApi.Filters;
using CDS.WebApi.Identity;

namespace CDS.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CandidateDocumentsController : Controller
    {
        private readonly IDbContext _ctx;

        public CandidateDocumentsController(IDbContext ctx)
        {
            _ctx = ctx;
        }

        // GET api/CandidateDocuments/aaf808d0-76cc-45fe-a3e6-4dd305198488
        [HttpGet("{cid}")]
        [ApiKeyAuthorize]
        public IActionResult Get(string cid)
        {
            Guid candidateId = Guid.Empty;
            if (!Guid.TryParse(cid, out candidateId))
                return BadRequest();

            return Json(_ctx.CandidateDocuments
                .Where(doc => doc.CandidateId == candidateId)
                .ToArray()
                .Select(doc => doc.ToViewModel())
                );
        }

        // POST api/values
        [HttpPost("{CId}")]
        [ApiKeyAuthorize]
        public IActionResult Post(string CId, [FromQuery] CDPostRequest model)
        {
            if (Request.Form.Files.Count == 0)
                ModelState.AddModelError("files", "File is required.");

            Guid candidateId = Guid.Empty;
            if (!Guid.TryParse(CId, out candidateId))
                ModelState.AddModelError("CandidateId", "Invalid value");

            if (IsEmployerModuleActive && !model.EId.HasValue)
                ModelState.AddModelError("EmployerId", "EmployerId is required.");

            if (ModelState.IsValid)
            {
                var file = Request.Form.Files[0];
                using (var readStream = file.OpenReadStream())
                {
                    byte[] fileData = new byte[file.Length];
                    readStream.Read(fileData, 0, (int)file.Length);

                    var doc = new CandidateDocument
                    {
                        CandidateId = candidateId,
                        DocumentType = model.Dt.Value,
                        FileName = file.FileName,
                        CreationDate = DateTime.UtcNow,
                        Content = fileData,
                        FileExtension = Path.GetExtension(file.FileName)
                    };
                    _ctx.CandidateDocuments.Add(doc);

                    return Json(CDPostResponse.Create(doc));
                }
            }

            return Json(CDPostResponse.Create(ModelState));
        }

        public bool IsEmployerModuleActive
        {
            get
            {
                var identity =  User.Identities.SingleOrDefault(x => x.Name == typeof(ApiKeyIdentity).Name) as ApiKeyIdentity;
                if (identity != null)
                    return identity.IsEmployerModuleActive;

                return false;
            }
        }
    }
}
