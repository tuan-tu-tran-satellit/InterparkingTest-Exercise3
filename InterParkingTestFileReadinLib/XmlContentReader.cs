using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace InterParkingTestFileReadinLib
{
    /// <summary>
    /// A class that can read and validate xml content
    /// </summary>
    public class XmlContentReader
    {
        readonly IFileReader _reader;

        public XmlContentReader(IFileReader reader = null)
        {
            _reader = reader ?? new FileReader();
        }

        public string ReadFileContent(string path)
        {
            var content = _reader.ReadFileContent(path);
            try
            {
                XDocument.Parse(content);
            }
            catch(Exception ex)
            {
                throw new InvalidDataException("File " + path + " does not contain xml content", ex);
            }
            return content;
        }
    }
}
