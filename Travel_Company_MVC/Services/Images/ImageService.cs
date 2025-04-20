using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System.IO;
using Travel_Company_MVC.Settings;
using TravelCompany.Domain.Const;

namespace Travel_Company_MVC.Services.Images
{
    internal class ImageService : IImageService
    {


        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Cloudinary _cloudinary;

        public ImageService(IWebHostEnvironment webHostEnvironment, Cloudinary cloudinary)
        {
            _webHostEnvironment = webHostEnvironment;
            _cloudinary = cloudinary;
        }

        public async Task<(bool IsUploadet, string? ErrorMessage, string? ImagePath, string? ThumbPath)> UploadImageAsync(IFormFile image,
            string folderDirectory, string? imageName = null, bool HasThumbnail=false)
        {

            // I add imageName parameter to the function so that i keep the image name and do not creat new Guid to the Edited image
            // so i will update the ImageUrl and ThumbImageUrl in the database ...

            if (image.Length > ImageProperties.MaxSizeAllowed)
                return (false, Errors.MaxSize ,null,null);

            var extintion=Path.GetExtension(image.FileName);

            if (!ImageProperties.AllowedExtensions.Contains(extintion))
                return (false, Errors.NotAllowedExtension,null,null);

            if(imageName== null)
                imageName = $"{Guid.NewGuid()}{extintion}";


            var path = Path.Combine($"{_webHostEnvironment.WebRootPath}{folderDirectory}", imageName);

            using(var stream=File.Create(path))
            {
                await image.CopyToAsync(stream);
            }

            if (HasThumbnail)
            {

                var thumbPath = Path.Combine($"{_webHostEnvironment.WebRootPath}{folderDirectory}/thumb", imageName);

                // Resize the Image using ImageShap.web Library ....

                using(var thumbImage= await Image.LoadAsync(image.OpenReadStream()))
                {

                    var ratio = (float)thumbImage.Width / 200;
                    var height = thumbImage.Height / ratio;

                    thumbImage.Mutate(i => i.Resize(width: 200, height: (int)height));


                    thumbImage.Save(thumbPath);
                }

                return (true, null, path, thumbPath);
            }


            return (true, null , path, null);
        }

        public void DeleteImage(string imagePath,string? thumbnailPath)
        {

            var fullImagePath = $"{_webHostEnvironment.WebRootPath}{imagePath}";

            if(File.Exists(fullImagePath))
                File.Delete(fullImagePath);


            if(thumbnailPath != null)
            {
                if (File.Exists(thumbnailPath))
                    File.Delete(thumbnailPath);
            }

        }


        public async Task<(bool IsUploadet, string? ErrorMessage, string? ImagePath, string? ThumbPath)> UploadCloudinaryImageAsync(IFormFile image, bool HasThumbnail = false)
        {

            if (image.Length > ImageProperties.MaxSizeAllowed)
                return (false, Errors.MaxSize, null, null);

            var extintion = Path.GetExtension(image.FileName);

            if (!ImageProperties.AllowedExtensions.Contains(extintion))
                return (false, Errors.NotAllowedExtension, null, null);

           
            var imageName = $"{Guid.NewGuid()}{extintion}";


            using (var stream = image.OpenReadStream())
            {
                var imageParams = new ImageUploadParams
                {
                    File = new FileDescription(imageName, stream),
                    UseFilename = true
                };

                var result =await _cloudinary.UploadAsync(imageParams);

                if (HasThumbnail)
                {
                    var url = result.SecureUrl.ToString();

                    return (true, null, url, GetThumbnailUrl(url));
                }

                return (true, null, result.SecureUrl.ToString(), null);
            }
  

        }

        public async Task DeleteCloudinaryImageAsync(string publicId)
        {
            await _cloudinary.DeleteResourcesAsync(publicId);
        }

        private string GetThumbnailUrl(string url)
        {
            var separator = "image/upload/";

            var urlParts = url.Split(separator);

            return $"{urlParts[0]}{separator}c_thumb,w_200,g_face/{urlParts[1]}";
        }

    }
}
