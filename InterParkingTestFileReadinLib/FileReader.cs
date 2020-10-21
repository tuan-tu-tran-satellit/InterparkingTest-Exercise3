using System;
using System.IO;

namespace InterParkingTestFileReadinLib
{
    public class FileReader : IFileReader
    {
        public string ReadFileContent(string path)
        {
            return File.ReadAllText(path); //ignoring encoding issues
        }
    }
}
