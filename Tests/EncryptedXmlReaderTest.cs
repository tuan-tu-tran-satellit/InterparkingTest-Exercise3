using FluentAssertions;
using InterParkingTestFileReadinLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tests
{
    [TestClass]
    public class EncryptedXmlReaderTest
    {
        [TestMethod]
        public void ItCanDecryptAndValidateXmlContent()
        {
            //Arrange
            var fakeReader = new FakeReader { Content = ">/raboof<" };
            var encryptedXmlReader = new EncryptedXmlFileReader(fileReader: fakeReader);

            //Act
            var result = encryptedXmlReader.ReadFileContent("some/path.xml.encrypted");

            //Assert
            result.Should().Be("<foobar/>");
        }

        [TestMethod]
        public void ItRejectsNonXmlContent()
        {
            //Arrange
            var fakeReader = new FakeReader { Content = "some encrypted but not xml content" };
            var encryptedXmlReader = new EncryptedXmlFileReader(fileReader: fakeReader);

            //Act
            Action readingTheFile = () => encryptedXmlReader.ReadFileContent("some/path");

            //Assert
            readingTheFile.Should().Throw<InvalidDataException>();
        }
    }
}
