using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WOPIHost
{
    interface IFileStorage
    {
        long GetFileSize(string name);

        DateTime? GetLastModifiedTime(string name);

        Stream GetFile(string name);

        int UploadFile(string name, Stream stream);

        List<string> GetFileNames();
             
    }
}
