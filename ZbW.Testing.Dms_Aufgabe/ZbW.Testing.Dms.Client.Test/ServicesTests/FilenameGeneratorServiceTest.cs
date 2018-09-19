using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Services.Impl;

namespace ZbW.Testing.Dms.Client.Test.ServicesTests
{
  [TestFixture]
  public class FilenameGeneratorServiceTest
  {
    [Test]
    public void GetContentFilename_CheckSuccess_ReturnTrue()
    {
      //arrange
      var filenameGeneratorServiceServiceStub = A.Fake< FilenameGeneratorServiceService> ();
      var documentId = new Guid("34CB2DFC-EB7F-45BB-8E82-C7CC9A30F871");
      var extension = ".pdf";

      //act
      var sut = filenameGeneratorServiceServiceStub.GetContentFilename(documentId, extension);

      //assert
      Assert.AreEqual(documentId + "_Content" + extension, sut);
    }

    [Test]
    public void GetContentFilename_ExtensionIsNull_ReturnFalse()
    {
      //arrange
      var filenameGeneratorServiceServiceStub = A.Fake<FilenameGeneratorServiceService>();
      var documentId = new Guid("34CB2DFC-EB7F-45BB-8E82-C7CC9A30F871");
      string extension = null;

      //act
      var sut = filenameGeneratorServiceServiceStub.GetContentFilename(documentId, extension);

      //assert
      Assert.IsNull(sut);
    }

    [Test]
    public void GetMetadataFilename_CheckSuccess_ReturnTrue()
    {
      //arrange
      var filenameGeneratorServiceServiceStub = A.Fake<FilenameGeneratorServiceService>();
      var documentId = new Guid("34CB2DFC-EB7F-45BB-8E82-C7CC9A30F871");

      //act
      var sut = filenameGeneratorServiceServiceStub.GetMetadataFilename(documentId);

      //assert
      Assert.AreEqual(documentId + "_Metadata.xml", sut);
    }
  }
}
