using Services.Abstraction;

namespace ECommerce.API
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageHelper(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Saves the uploaded image file to wwwroot/uploads/{subFolder} and returns the relative path to store in DB
        /// </summary>
        public async Task<string> SaveImageAsync(IFormFile imageFile, string subFolder)
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("Image file is required.");

            var fileExtension = Path.GetExtension(imageFile.FileName);
            var fileName = $"{Guid.NewGuid()}{fileExtension}";

            //var folderPath = Path.Combine(_env.WebRootPath, "uploads", subFolder);
            //var folderPath = Path.Combine(_env.WebRootPath, "Uploads", "Images", subFolder);
            var webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var folderPath = Path.Combine(webRootPath, "Uploads", "Images", subFolder);

            Directory.CreateDirectory(folderPath); // create if not exists

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return relative path (to be stored in DB)
            //return Path.Combine("uploads", subFolder, fileName).Replace("\\", "/");
            return Path.Combine("Uploads", "Images", subFolder, fileName).Replace("\\", "/");
        }

        /// <summary>
        /// Returns the full URL to access an image via browser
        /// </summary>
        //public string GetImageUrl(string fileNameWithPath)
        //{
        //    var request = _httpContextAccessor.HttpContext?.Request;
        //    var baseUrl = $"{request?.Scheme}://{request?.Host}";
        //    return $"{baseUrl}/{fileNameWithPath.Replace("\\", "/")}";
        //}

        public string GetImageUrl(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return null;

            // Already absolute? – just return it
            if (Uri.IsWellFormedUriString(relativePath, UriKind.Absolute))
                return relativePath;

            relativePath = relativePath.Replace("\\", "/").TrimStart('/');

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return "/" + relativePath;             // background threads

            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            return $"{baseUrl}/{relativePath}";
        }

        /// <summary>
        /// Deletes an image from wwwroot/Uploads/Images/... based on its relative path.
        /// </summary>
        public bool DeleteImage(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return false;

            var webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            // Normalize relative path (remove leading slash if exists)
            relativePath = relativePath.Replace("\\", "/").TrimStart('/');

            var fullPath = Path.Combine(webRootPath, relativePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }

            return false; // file not found
        }

    }
}
