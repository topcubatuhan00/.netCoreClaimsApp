using Business.Abstract;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Business.Conrete
{
    public class FileManager : IFileService
    {
        public void FileDeleteToFtp(string path)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp adresi" + path);
                request.Credentials = new NetworkCredential("kullanıcı adı","şifre");
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (Exception) { }  
        }

        public void FileDeleteToServer(string path)
        {
            try
            {
                if(System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }
            catch (Exception) { }
        }

        public string FileSave(IFormFile file, string path)
        {
            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf("."));
            fileFormat = fileFormat.ToLower();

            string fileName = Guid.NewGuid().ToString() + fileFormat;

            string filePath = path + fileName;
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }
            return fileName;

        }

        public string FileSaveToFtp(IFormFile file)
        {
            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf("."));
            fileFormat = fileFormat.ToLower();

            string fileName = Guid.NewGuid().ToString() + fileFormat;



            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp adresi" + fileName);
            request.Credentials = new NetworkCredential("kullanıcı adı", "şifre");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream ftpStream = request.GetRequestStream())
            {
                file.CopyTo(ftpStream);
            }
            return fileName;
        }
    }
}
