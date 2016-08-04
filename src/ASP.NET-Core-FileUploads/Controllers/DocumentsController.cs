using ASP.NET_Core_FileUploads.Models.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;

namespace ASP.NET_Core_FileUploads.Controllers
{
    [Route("api/[controller]")]
    public class DocumentsController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public DocumentsController(
            IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] DocumentInput upload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var file = upload.File;
            var parsedContentDisposition =
                ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            var filename = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", parsedContentDisposition.FileName.Trim('"'));

            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                var inputStream = upload.File.OpenReadStream();
                await inputStream.CopyToAsync(fileStream);
            }

            return Ok();
        }
    }
}
