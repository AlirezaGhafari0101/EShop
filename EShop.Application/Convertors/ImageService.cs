using EShop.Application.Generator;
using EShop.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Convertors
{
    public static class ImageService
    {
        public static string CreateImage(IFormFile image, string entityLocation, string imageName = "default.png")
        {

           
            if (image != null)
            {
                imageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(image.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{entityLocation}", imageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }
            return imageName;


        }
    }
}
