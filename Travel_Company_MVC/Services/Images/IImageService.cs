using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Company_MVC.Services.Images
{
    public interface IImageService
    {


        Task<(bool IsUploadet,string? ErrorMessage, string? ImagePath, string? ThumbPath)> UploadImageAsync(IFormFile image , string folderPath, string? imageName , bool HasThumbnail);
        Task<(bool IsUploadet, string? ErrorMessage, string? ImagePath, string? ThumbPath)> UploadCloudinaryImageAsync(IFormFile image, bool HasThumbnail);

        void DeleteImage(string imagePath, string? thumbnailPath);
        Task DeleteCloudinaryImageAsync(string publicId);
    }
}
