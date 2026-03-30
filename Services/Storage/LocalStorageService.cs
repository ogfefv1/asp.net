
namespace AspKnP231.Services.Storage
{
    public class LocalStorageService : IStorageService
    {
        private readonly String _path;

        public LocalStorageService()
        {
            _path = Path.Combine(Directory.GetCurrentDirectory(), "LocalStorage/");
        }

        public byte[] Load(string filename)
        {
            String savedName = Path.Combine(_path, filename);
            if (File.Exists(savedName))
            {
                return File.ReadAllBytes(savedName);
            }
            throw new FileNotFoundException(savedName);
        }

        public string Save(IFormFile formFile)
        {
            String ext = Path.GetExtension(formFile.FileName);
            String savedName = Guid.NewGuid().ToString() + ext;
            using Stream stream = File.OpenWrite(Path.Combine(_path, savedName));
            formFile.CopyTo(stream);
            return savedName;
        }
    }
}
