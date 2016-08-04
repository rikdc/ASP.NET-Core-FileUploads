using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_FileUploads.Models.Dto
{
    public class DocumentInput
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
