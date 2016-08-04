using ASP.NET_Core_FileUploads.Models;
using ASP.NET_Core_FileUploads.Models.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ASP.NET_Core_FileUploads.Repository
{
    public class DocumentsRepository : IDocumentsRepository
    {
        private IHostingEnvironment _hostingEnvironment;

        public DocumentsRepository(
            IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<Document> Create(DocumentInput input)
        {
            var file = input.File;
            var parsedContentDisposition =
                ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            var parsedFilename = HeaderUtilities.RemoveQuotes(parsedContentDisposition.FileName);
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(parsedFilename);

            var fileDestination = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads", filename);

            var upload = new Document()
            {
                Name = input.Name,
                PathToFile = fileDestination
            };

            using (var fileStream = new FileStream(fileDestination, FileMode.Create))
            {
                var inputStream = file.OpenReadStream();
                await inputStream.CopyToAsync(fileStream);
            }

            return upload;
        }
    }
}
