using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterParkingTestFileReadinLib
{
    public class EncryptedFileReader : IFileReader
    {
        private readonly Func<string, string> _algorithm;
        private readonly IFileReader _fileReader;

        public EncryptedFileReader(Func<string, string> algorithm = null, IFileReader fileReader = null)
        {
            _algorithm = algorithm;
            if(_algorithm == null)
            {
                _algorithm = s => new String(s.Reverse().ToArray());
            }
            _fileReader = fileReader ?? new FileReader();
        }

        public string ReadFileContent(string path)
        {
            var encryptedContent = _fileReader.ReadFileContent(path);
            var clearContent = _algorithm(encryptedContent);
            return clearContent;
        }
    }
}
