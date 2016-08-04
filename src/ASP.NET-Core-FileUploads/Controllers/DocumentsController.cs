using ASP.NET_Core_FileUploads.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.NET_Core_FileUploads.Controllers
{
    public class DocumentsController
    {
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

            var filename = Path.Combine("uploads", parsedContentDisposition.FileName.Trim('"'));

            var cancellationToken = default(CancellationToken);

            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                var inputStream = upload.File.OpenReadStream();
                await inputStream.CopyToAsync(fileStream, 81920, cancellationToken);
            }

            return Ok();
        }
    }
}
