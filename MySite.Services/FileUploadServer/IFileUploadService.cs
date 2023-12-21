using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MySite.Services.FileUploadServer
{
   public interface IFileUploadService
    {
      Task<string> UploadFileOnServer(IFormFile file);
    }
}
