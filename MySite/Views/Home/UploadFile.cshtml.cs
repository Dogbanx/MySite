using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySite.Services.FileUploadServer;
using Serilog;

namespace MySite.Views.Home
{
    public class UploadFileModel : PageModel
    {
        private readonly ILogger<UploadFileModel> _logger;
        private readonly IFileUploadService _uploadServiceOnServer;

        public string FilePath;

        public UploadFileModel(ILogger<UploadFileModel> logger, IFileUploadService uploadServiceOnServer)
        {
            _logger = logger;
            this._uploadServiceOnServer = uploadServiceOnServer;
        }
        public void OnGet()
        {
        }
        public async Task OnPost(IFormFile file)
        {
            Log.Information("OnPost");
            if (file != null)
            {
                FilePath = await _uploadServiceOnServer.UploadFileOnServer(file);
            }
        }
    }
}
