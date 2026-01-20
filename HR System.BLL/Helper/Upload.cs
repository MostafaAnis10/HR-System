using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_System.BLL.Helper
{
    public static class Upload
    {

        public static string UploadFile(string FolderName, IFormFile File)
        {
            try
            {
                // 1) Get Directory
                // catch the folder Path and the file name in server
                string FolderPath = Directory.GetCurrentDirectory() + "/wwwroot/" + FolderName;

                // 2) Get File Name
                // Guid => Word contain from 36 character
                string FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);

                // 3) Merge Path with File Name
                // combine put
                string FinalPath = Path.Combine(FolderPath, FileName);

                // 4) Save File As Streams "Data Overtime"
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    File.CopyTo(Stream);
                }

                // return name saved in server
                return FileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string RemoveFile(string FolderName, string fileName)
        {
            try
            {
                // get Directory => path of file in server
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FolderName, fileName);


                if (File.Exists(directory))
                {
                    File.Delete(directory);
                    return "File Deleted";
                }

                return "File Not Deleted";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
