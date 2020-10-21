using FluentAssertions;
using InterParkingTestFileReadinLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Tests
{
    [TestClass]
    public partial class XmlContentReaderTest
    {

        FakeReader _fakeReader;
        [TestInitialize]
        public void Init()
        {
            _fakeReader = new FakeReader();
        }

        [TestMethod]
        public void ItCanReadXmlContent()
        {
            var content = "<foo><bar/></foo>";
            _fakeReader.Content = content;

            var xmlReader = new XmlContentReader(_fakeReader);

            var result = xmlReader.ReadFileContent("some/path");

            result.Should().Be(content);
        }

        [TestMethod]
        public void ItCanDetectInvalidXml()
        {
            var content = "this is not xml";
            _fakeReader.Content = content;

            var xmlReader = new XmlContentReader(_fakeReader);

            Action read = () => xmlReader.ReadFileContent("some/content");

            read.Should().Throw<InvalidDataException>();
        }
    }
}
