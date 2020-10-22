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
    public class EncryptedJsonReaderTest
    {
        [TestMethod]
        public void ItCanDecryptAndValidateJsonContent()
        {
            //Arrange
            var fakeReader = new FakeReader { Content = "}llun:'oof'{" };
            var encryptedJsonReader = new EncryptedJsonFileReader(fileReader: fakeReader);

            //Act
            var result = encryptedJsonReader.ReadFileContent("some/path.json.encrypted");

            //Assert
            result.Should().Be("{'foo':null}");
        }

        [TestMethod]
        public void ItRejectsNonJsonContent()
        {
            //Arrange
            var fakeReader = new FakeReader { Content = "some encrypted but not json content" };
            var encryptedJsonReader = new EncryptedJsonFileReader(fileReader: fakeReader);

            //Act
            Action readingTheFile = () => encryptedJsonReader.ReadFileContent("some/path");

            //Assert
            readingTheFile.Should().Throw<InvalidDataException>();
        }
    }
}
