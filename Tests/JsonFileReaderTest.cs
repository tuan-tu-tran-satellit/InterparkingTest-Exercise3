using FluentAssertions;
using InterParkingTestFileReadinLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Tests
{
    [TestClass]
    public partial class JsonFileReaderTest
    {
        [TestMethod]
        public void ItCanReadJsonFile()
        {
            var content = "{'foo':'bar'}";
            var fakeReader = new FakeReader { Content = content };

            var xmlReader = new JsonFileReader(fakeReader);

            var result = xmlReader.ReadFileContent("some/path");

            result.Should().Be(content);
        }

        [TestMethod]
        [DataRow("this is not json")]
        [DataRow("<neither>is this</neither>")]
        public void ItCanDetectInvalidJson(string invalidContent)
        {
            var fakeReader = new FakeReader { Content = invalidContent };

            var xmlReader = new JsonFileReader(fakeReader);

            Action read = () => xmlReader.ReadFileContent("some/content");

            read.Should().Throw<InvalidDataException>();
        }
    }
}
