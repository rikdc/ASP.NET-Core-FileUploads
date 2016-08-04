using ASP.NET_Core_FileUploads.Models.Dto;
using ASP.NET_Core_FileUploads.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.NET_Core_FileUploads.Controllers
{
    [Route("api/[controller]")]
    public class DocumentsController : Controller
    {
        private IDocumentsRepository _documentRepository;

        public DocumentsController(
            IDocumentsRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] DocumentInput upload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _documentRepository.Create(upload);

            return Ok(result);
        }
    }
}
