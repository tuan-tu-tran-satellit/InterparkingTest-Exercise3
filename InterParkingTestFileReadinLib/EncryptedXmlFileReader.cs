using System;
using System.Collections.Generic;
using System.Text;

namespace InterParkingTestFileReadinLib
{
    /// <summary>
    /// A reader that reads encrypted files and validates that the (clear) content is actually xml
    /// </summary>
    public class EncryptedXmlFileReader : IFileReader
    {
        private readonly XmlContentReader _reader;

        public EncryptedXmlFileReader(Func<string, string> algorithm = null, IFileReader fileReader = null)
        {
            _reader = new XmlContentReader(
                new EncryptedFileReader(algorithm, fileReader)
            );
        }

        public string ReadFileContent(string path)
        {
            return _reader.ReadFileContent(path);
        }
    }
}
