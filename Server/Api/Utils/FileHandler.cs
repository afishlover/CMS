using Api.Interfaces;

namespace Api.Utils
{
    public class FileHandler : IFileHandler
    {
        public Task Delete(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                dir.Delete(true);
            }
            return Task.CompletedTask;
        }


        public async Task<string?> Upload(IFormFile file, string path, string fileName)
        {
            try
            {
                if (file.Length > 0)
                {
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), path);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(path, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return dbPath;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
