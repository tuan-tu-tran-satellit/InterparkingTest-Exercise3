using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace InterParkingTestFileReadinLib
{
    /// <summary>
    /// A file reader that consults a security context before reading a file.
    /// And then validates that the file content is indeed xml.
    /// </summary>
    public class RestrictedXmlFileReader
    {
        private readonly XmlContentReader _fileReader;
        private readonly ISecurityContext _securityContext;

        public RestrictedXmlFileReader(ISecurityContext securityContext, IFileReader fileReader = null)
        {
            _fileReader = new XmlContentReader(fileReader);
            _securityContext = securityContext;
        }
        public string ReadFileContent(string path)
        {
            if (_securityContext.AllowsToReadFile(path, out var reasonForDenial))
                return _fileReader.ReadFileContent(path);
            else
            {
                throw new SecurityException("Current security context does not allow to read file " + path + " : " + reasonForDenial);
            }
        }
    }
}
