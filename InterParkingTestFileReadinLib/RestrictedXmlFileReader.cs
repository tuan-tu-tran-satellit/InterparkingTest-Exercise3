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
    public class RestrictedXmlFileReader : RestrictedFileReader
    {
        public RestrictedXmlFileReader(ISecurityContext securityContext, IFileReader fileReader = null)
            : base(securityContext, new XmlContentReader(fileReader))
        {
        }
    }
}
