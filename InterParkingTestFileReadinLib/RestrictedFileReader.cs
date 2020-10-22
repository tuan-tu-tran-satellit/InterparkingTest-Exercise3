using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace InterParkingTestFileReadinLib
{
    /// <summary>
    /// A file reader that consults a security context before reading a file (using another file reader).
    /// </summary>
    public class RestrictedFileReader : IFileReader
    {
        private readonly IFileReader _fileReader;
        private readonly ISecurityContext _securityContext;

        public RestrictedFileReader(ISecurityContext securityContext, IFileReader fileReader = null)
        {
            _fileReader = fileReader;
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
