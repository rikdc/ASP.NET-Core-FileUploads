using ASP.NET_Core_FileUploads.Models;
using ASP.NET_Core_FileUploads.Models.Dto;
using System.Threading.Tasks;

namespace ASP.NET_Core_FileUploads.Repository
{
    public interface IDocumentsRepository
    {
        Task<Document> Create(DocumentInput input);
    }
}