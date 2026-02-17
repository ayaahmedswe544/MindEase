using Microsoft.AspNetCore.Http;
namespace MindEase.IService
{
    public interface IImageService
    {

        Task<string> UploadImageAsync(IFormFile file, string folderName = "MemoryPhotos");

    }
}
