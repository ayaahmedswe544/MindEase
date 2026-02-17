using MindEase.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace MindEase.Service
{
    public class ImageService:IImageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string[] _allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        private const long _maxFileSize = 5 * 1024 * 1024; // 5 MB

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> UploadImageAsync(IFormFile file, string folderName = "uploads")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file provided");

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!_allowedExtensions.Contains(extension))
                throw new ArgumentException("Invalid file type. Allowed: " + string.Join(", ", _allowedExtensions));

            if (file.Length > _maxFileSize)
                throw new ArgumentException($"File size exceeds {_maxFileSize / (1024 * 1024)} MB limit.");

            string folder = Path.Combine(_env.WebRootPath, folderName);
            Directory.CreateDirectory(folder);
            string fileName = Guid.NewGuid() + extension;
            string fullPath = Path.Combine(folder, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/{folderName}/{fileName}";
            //var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{folderName}/{fileName}";
        }
    }
}
