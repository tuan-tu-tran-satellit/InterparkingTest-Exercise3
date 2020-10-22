using System;
using System.Collections.Generic;
using System.Text;

namespace InterParkingTestFileReadinLib
{
    public class EncryptedJsonFileReader : IFileReader
    {
        private readonly JsonFileReader _reader;

        public EncryptedJsonFileReader(Func<string, string> algorithm = null, IFileReader fileReader = null)
        {
            _reader = new JsonFileReader(new EncryptedFileReader(algorithm, fileReader));
        }

        public string ReadFileContent(string path)
        {
            return _reader.ReadFileContent(path);
        }
    }
}
