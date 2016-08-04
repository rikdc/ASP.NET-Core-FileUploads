namespace ASP.NET_Core_FileUploads.Models.Dto
{
    public class DocumentInput
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        [FileExtensions(Extensions = "jpg,jpeg,doc,docx,pdf")]
        public IFormFile File { get; set; }
    }
}
