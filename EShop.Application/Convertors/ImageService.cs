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
        public static string CreateImage(IFormFile Avatar, string userAvatarName)
        {

           
            if (Avatar != null)
            {
                userAvatarName = NameGenerator.GenerateUniqCode() + Path.GetExtension(Avatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvatar", userAvatarName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Avatar.CopyTo(stream);
                }
            }
            return userAvatarName;


        }
    }
}
