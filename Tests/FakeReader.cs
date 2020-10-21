using InterParkingTestFileReadinLib;

namespace Tests
{
    public partial class XmlContentReaderTest
    {
        class FakeReader : IFileReader
        {
            public string Content = "";
            public string ReadFileContent(string path)
            {
                return Content;
            }
        }
    }
}
