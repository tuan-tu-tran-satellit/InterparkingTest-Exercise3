using InterParkingTestFileReadinLib;

namespace Tests
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
