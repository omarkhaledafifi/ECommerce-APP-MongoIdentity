using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IImageHelper
    {
        /// <summary>
        /// Saves the uploaded image and returns the relative path (e.g., to store in DB)
        /// </summary>
        Task<string> SaveImageAsync(IFormFile imageFile, string subFolder);

        /// <summary>
        /// Converts a stored relative path to a full URL (to return to clients)
        /// </summary>
        string GetImageUrl(string relativePath);
        /// <summary>
        /// Deletes an image given its relative path
        /// </summary>
        /// <returns>returns true is the image exists and successfully deleted and false otherwise</returns>
        bool DeleteImage(string relativePath);
    }
}
