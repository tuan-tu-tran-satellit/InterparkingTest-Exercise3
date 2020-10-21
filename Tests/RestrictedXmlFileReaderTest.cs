using FluentAssertions;
using InterParkingTestFileReadinLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;

namespace Tests
{
    [TestClass]
    public class RestrictedXmlFileReaderTest
    {
        [TestMethod]
        public void ItCanEnforceASecurityPolicy()
        {
            //Arrange
            var securityPolicy = new Mock<ISecurityContext>(MockBehavior.Strict);

            var somePath = "/some/path.xml";
            var reasonForDenial = "this user cannot read anything";
            securityPolicy.Setup(s => s.AllowsToReadFile(somePath, out reasonForDenial)).Returns(false);

            var restrictedReader = new RestrictedXmlFileReader(securityPolicy.Object);

            //Act
            Action readingTheFile = () => restrictedReader.ReadFileContent(somePath);

            //Assert
            readingTheFile.Should().Throw<SecurityException>()
                .Which.Message.Should().Contain(somePath).And.Contain(reasonForDenial);
        }

        [TestMethod]
        public void It_Can_Read_XmlFiles_If_The_SecurityContext_Allows_It()
        {
            //Arrange
            var securityPolicy = new Mock<ISecurityContext>(MockBehavior.Strict);

            var somePath = "/some/path.xml";
            string reasonForDenial = null;
            securityPolicy.Setup(s => s.AllowsToReadFile(somePath, out reasonForDenial)).Returns(true);

            var fakeReader = new FakeReader { Content = "<some_xml/>" };

            var restrictedReader = new RestrictedXmlFileReader(securityPolicy.Object, fakeReader);

            //Act
            var result = restrictedReader.ReadFileContent(somePath);

            //Assert
            result.Should().Be(fakeReader.Content);
        }

        [TestMethod]
        public void It_Validates_The_XmlContent_When_Allowed_By_The_SecurityContext()
        {
            //Arrange
            var securityPolicy = new Mock<ISecurityContext>(MockBehavior.Strict);

            var somePath = "/some/path.txt";
            string reasonForDenial = null;
            securityPolicy.Setup(s => s.AllowsToReadFile(somePath, out reasonForDenial)).Returns(true);

            var fakeReader = new FakeReader { Content = "not xml content" };

            var restrictedReader = new RestrictedXmlFileReader(securityPolicy.Object, fakeReader);

            //Act
            Action readingTheFile = () => restrictedReader.ReadFileContent(somePath);

            //Assert
            readingTheFile.Should().Throw<InvalidDataException>();
        }
    }
}
