using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace MySite.Services.FileUploadServer
{
    public class LocalFileUploadService: IFileUploadService
    {
      
        private readonly IHostingEnvironment enviroment;

        
        public LocalFileUploadService(IHostingEnvironment enviroment)
        {
            this.enviroment = enviroment;
        }

       
        public async Task<string> UploadFileOnServer(IFormFile file) {

            var filePath = Path.Combine(enviroment.ContentRootPath, @"wwwroot\infornation", file.FileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return filePath;
        }
    }
}
