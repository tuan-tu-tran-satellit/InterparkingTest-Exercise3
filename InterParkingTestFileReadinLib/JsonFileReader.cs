using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InterParkingTestFileReadinLib
{

    public class JsonFileReader : IFileReader
    {
        private readonly IFileReader _fileReader;

        public JsonFileReader(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public string ReadFileContent(string path)
        {
            var content = _fileReader.ReadFileContent(path);

            try
            {
                JsonConvert.DeserializeObject(content);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("File " + path + " is not a valid json file", ex);
            }
            return content;
        }
    }
}
