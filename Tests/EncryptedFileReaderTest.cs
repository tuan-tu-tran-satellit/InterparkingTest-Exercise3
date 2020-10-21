using FluentAssertions;
using InterParkingTestFileReadinLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class EncryptedFileReaderTest
    {
        [TestMethod]
        public void ItCanReadEncyptedContent()
        {
            //Arrange
            var fakeReader = new FakeReader() { Content = "raboof" };
            var encryptedFileReader = new EncryptedFileReader(fileReader: fakeReader);

            //Act
            var result = encryptedFileReader.ReadFileContent("some/path");

            //Assert
            result.Should().Be("foobar");
        }
    }
}
