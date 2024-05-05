
using Microsoft.AspNetCore.Http;

namespace MyPortfolio.BAL.Helpers
{
    public static class FileHelper
    {
        public static async Task<String> CreateFile(IFormFile file)
        {
            var format = Path.GetExtension(file.FileName);
            var fileName = file.FileName.Replace(format, "");
            fileName = fileName.Replace(" ", "");
            var randomName = string.Format($"{fileName}_{Guid.NewGuid()}{format}");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\gallery", randomName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return randomName;
        }
        public static async Task<String> ReplaceFile(string oldImgName, IFormFile file)
        {
            DeleteFile(oldImgName);
            var ImgName = await CreateFile(file);
            return ImgName;
        }
        public static void DeleteFile(string ImgName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), ("wwwroot/img/gallery/"), ImgName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
