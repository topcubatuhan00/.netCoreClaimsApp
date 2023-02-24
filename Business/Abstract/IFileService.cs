using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IFileService
    {
        string FileSave(IFormFile file, string path);
        string FileSaveToFtp(IFormFile file);
        void FileDeleteToServer(string path);
        void FileDeleteToFtp(string path);
    }
}
