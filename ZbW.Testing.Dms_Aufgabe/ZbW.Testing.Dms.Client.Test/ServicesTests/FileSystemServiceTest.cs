using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Services;
using ZbW.Testing.Dms.Client.Services.Impl;
using ZbW.Testing.Dms.Client.Test.StubFactory;

namespace ZbW.Testing.Dms.Client.Test.ServicesTests
{
  [TestFixture]
  public class FileSystemServiceTest
  {
    [Test]
    public void AddFile_GenerateNewGuid_ReturnTrue()
    {
      var documentId = new Guid("34CB2DFC-EB7F-45BB-8E82-C7CC9A30F871");

      //arrange
      var stubMetaDataItemFactory = new IMetaDataItemFactory();
      var metadataItemStub = stubMetaDataItemFactory.GetMetadataItem();

      var directoryServiceStub = A.Fake<IDirectoryService>();
      A.CallTo(() => directoryServiceStub.GetExtension(@"C:\Temp\sourcePath\bla.pdf")).Returns(".pdf");
      A.CallTo(() => directoryServiceStub.Combine("targetPath", "2018")).Returns(@"C:\Temp\targetPath\2018");
      A.CallTo(directoryServiceStub).WithVoidReturnType().DoesNothing();

      var guidGeneratorStub = A.Fake<IGuidGeneratorService>();
      A.CallTo(() => guidGeneratorStub.GetNewGuid()).Returns(documentId);

      var filenameGeneratorServiceStub = A.Fake<IFilenameGeneratorService>();
      A.CallTo(() => filenameGeneratorServiceStub.GetContentFilename(metadataItemStub.DocumentId, ".pdf")).Returns(metadataItemStub.DocumentId + "_content");
      A.CallTo(() => filenameGeneratorServiceStub.GetMetadataFilename(metadataItemStub.DocumentId)).Returns(metadataItemStub.DocumentId + "_metadata");

      var xmlServiceStub = A.Fake<IXmlService>();
      A.CallTo(() => xmlServiceStub.MetadataItemToXml(metadataItemStub, "targetPath2018")).DoesNothing();

      var sut = new FileSystemService(xmlServiceStub, filenameGeneratorServiceStub, directoryServiceStub, guidGeneratorStub);

      //act
      sut.AddFile(metadataItemStub, false, @"C:\Temp\sourcePath\bla.pdf");

      //assert
      A.CallTo(() => guidGeneratorStub.GetNewGuid()).MustHaveHappened();
      Assert.AreEqual(documentId.ToString(), metadataItemStub.DocumentId.ToString());
    }



    [Test]
    public void AddFile_GetExtension_ReturnTrue()
    {
      var documentId = new Guid("34CB2DFC-EB7F-45BB-8E82-C7CC9A30F871");

      //arrange
      var stubMetaDataItemFactory = new IMetaDataItemFactory();
      var metadataItemStub = stubMetaDataItemFactory.GetMetadataItem();
  
      var directoryServiceStub = A.Fake<IDirectoryService>();
      A.CallTo(() => directoryServiceStub.GetExtension(@"C:\Temp\sourcePath\bla.pdf")).Returns(".pdf");
      A.CallTo(() => directoryServiceStub.Combine("targetPath", "2018")).Returns(@"C:\Temp\targetPath\2018");
      A.CallTo(directoryServiceStub).WithVoidReturnType().DoesNothing();

      var guidGeneratorStub = A.Fake<IGuidGeneratorService>();
      A.CallTo(() => guidGeneratorStub.GetNewGuid()).Returns(metadataItemStub.DocumentId);

      var filenameGeneratorServiceStub = A.Fake<IFilenameGeneratorService>();
      A.CallTo(() => filenameGeneratorServiceStub.GetContentFilename(metadataItemStub.DocumentId, ".pdf")).Returns(metadataItemStub.DocumentId + "_content");
      A.CallTo(() => filenameGeneratorServiceStub.GetMetadataFilename(metadataItemStub.DocumentId)).Returns(metadataItemStub.DocumentId + "_metadata");

      var xmlServiceStub = A.Fake<IXmlService>();
      A.CallTo(() => xmlServiceStub.MetadataItemToXml(metadataItemStub, "targetPath2018")).DoesNothing();

      var sut = new FileSystemService(xmlServiceStub, filenameGeneratorServiceStub, directoryServiceStub, guidGeneratorStub);

      //act
      sut.AddFile(metadataItemStub, false, @"C:\Temp\sourcePath\bla.pdf");

      //assert
      A.CallTo(() => directoryServiceStub.GetExtension(@"C:\Temp\sourcePath\bla.pdf")).MustHaveHappened();
      Assert.AreEqual(".pdf", metadataItemStub.ContentFileExtension);
    }

    [Test]
    public void AddFile_FilenameGenerator_ReturnTrue()
    {
      var documentId = new Guid("34CB2DFC-EB7F-45BB-8E82-C7CC9A30F871");

      //arrange
      var stubMetaDataItemFactory = new IMetaDataItemFactory();
      var metadataItemStub = stubMetaDataItemFactory.GetMetadataItem();

      var directoryServiceStub = A.Fake<IDirectoryService>();
      A.CallTo(() => directoryServiceStub.GetExtension(@"C:\Temp\sourcePath\bla.pdf")).Returns(".pdf");
      A.CallTo(() => directoryServiceStub.Combine("targetPath", "2018")).Returns(@"C:\Temp\targetPath\2018");
      A.CallTo(directoryServiceStub).WithVoidReturnType().DoesNothing();

      var guidGeneratorStub = A.Fake<IGuidGeneratorService>();
      A.CallTo(() => guidGeneratorStub.GetNewGuid()).Returns(documentId);

      var filenameGeneratorServiceStub = A.Fake<IFilenameGeneratorService>();
      A.CallTo(() => filenameGeneratorServiceStub.GetContentFilename(metadataItemStub.DocumentId, ".pdf")).Returns(metadataItemStub.DocumentId + "_content");
      A.CallTo(() => filenameGeneratorServiceStub.GetMetadataFilename(metadataItemStub.DocumentId)).Returns(metadataItemStub.DocumentId + "_metadata");

      var xmlServiceStub = A.Fake<IXmlService>();
      A.CallTo(() => xmlServiceStub.MetadataItemToXml(metadataItemStub, "targetPath2018")).DoesNothing();

      var sut = new FileSystemService(xmlServiceStub, filenameGeneratorServiceStub, directoryServiceStub, guidGeneratorStub);

      //act
      sut.AddFile(metadataItemStub, false, @"C:\Temp\sourcePath\bla.pdf");

      //assert
      A.CallTo(() => filenameGeneratorServiceStub.GetContentFilename(metadataItemStub.DocumentId, ".pdf")).MustHaveHappened();
      A.CallTo(() => filenameGeneratorServiceStub.GetMetadataFilename(metadataItemStub.DocumentId)).MustHaveHappened();
      Assert.AreEqual(documentId + "_content", metadataItemStub.ContentFilename);
      Assert.AreEqual(documentId + "_metadata", metadataItemStub.MetadataFilename);
    }

    [Test]
    public void AddFile_CombinePath_ReturnTrue()
    {
      var documentId = new Guid("34CB2DFC-EB7F-45BB-8E82-C7CC9A30F871");

      //arrange
      var stubMetaDataItemFactory = new IMetaDataItemFactory();
      var metadataItemStub = stubMetaDataItemFactory.GetMetadataItem();

      var directoryServiceStub = A.Fake<IDirectoryService>();
      A.CallTo(() => directoryServiceStub.GetExtension(@"C:\Temp\sourcePath\bla.pdf")).Returns(".pdf");
      A.CallTo(() => directoryServiceStub.Combine(@"C:\Temp\DMS", "2018")).Returns(@"C:\Temp\DMS\2018");
      A.CallTo(directoryServiceStub).WithVoidReturnType().DoesNothing();

      var guidGeneratorStub = A.Fake<IGuidGeneratorService>();
      A.CallTo(() => guidGeneratorStub.GetNewGuid()).Returns(documentId);

      var filenameGeneratorServiceStub = A.Fake<IFilenameGeneratorService>();
      A.CallTo(() => filenameGeneratorServiceStub.GetContentFilename(metadataItemStub.DocumentId, ".pdf")).Returns(metadataItemStub.DocumentId + "_content");
      A.CallTo(() => filenameGeneratorServiceStub.GetMetadataFilename(metadataItemStub.DocumentId)).Returns(metadataItemStub.DocumentId + "_metadata");

      var xmlServiceStub = A.Fake<IXmlService>();
      A.CallTo(() => xmlServiceStub.MetadataItemToXml(metadataItemStub, "targetPath2018")).DoesNothing();

      var sut = new FileSystemService(xmlServiceStub, filenameGeneratorServiceStub, directoryServiceStub, guidGeneratorStub);

      //act
      sut.AddFile(metadataItemStub, false, @"C:\Temp\sourcePath\bla.pdf");

      //assert
      A.CallTo(() => directoryServiceStub.Combine(@"C:\Temp\DMS", "2018")).MustHaveHappened();
      Assert.IsTrue(metadataItemStub.PathInRepo.Contains((@"C:\Temp\DMS\2018")));
    }

    public void AddFile_CheckContentMetadata_ReturnTrue()
    {
      var documentId = new Guid("34CB2DFC-EB7F-45BB-8E82-C7CC9A30F871");

      //arrange
      var stubMetaDataItemFactory = new IMetaDataItemFactory();
      var metadataItemStub = stubMetaDataItemFactory.GetMetadataItem();

      var directoryServiceStub = A.Fake<IDirectoryService>();
      A.CallTo(() => directoryServiceStub.GetExtension(@"C:\Temp\sourcePath\bla.pdf")).Returns(".pdf");
      A.CallTo(() => directoryServiceStub.Combine(@"C:\Temp\DMS", "2018")).Returns(@"C:\Temp\DMS\2018");
      A.CallTo(directoryServiceStub).WithVoidReturnType().DoesNothing();

      var guidGeneratorStub = A.Fake<IGuidGeneratorService>();
      A.CallTo(() => guidGeneratorStub.GetNewGuid()).Returns(documentId);

      var filenameGeneratorServiceStub = A.Fake<IFilenameGeneratorService>();
      A.CallTo(() => filenameGeneratorServiceStub.GetContentFilename(metadataItemStub.DocumentId, ".pdf")).Returns(metadataItemStub.DocumentId + "_content");
      A.CallTo(() => filenameGeneratorServiceStub.GetMetadataFilename(metadataItemStub.DocumentId)).Returns(metadataItemStub.DocumentId + "_metadata");

      var xmlServiceStub = A.Fake<IXmlService>();
      A.CallTo(() => xmlServiceStub.MetadataItemToXml(metadataItemStub, "targetPath2018")).DoesNothing();

      var sut = new FileSystemService(xmlServiceStub, filenameGeneratorServiceStub, directoryServiceStub, guidGeneratorStub);

      //act
      sut.AddFile(metadataItemStub, false, @"C:\Temp\sourcePath\bla.pdf");

      //assert
      Assert.AreEqual(@"C:\Temp\sourcePath\bla.pdf", metadataItemStub.OrginalPath);
      Assert.AreEqual(@"C:\Temp\DMS\2018" + metadataItemStub.ContentFilename, metadataItemStub.PathInRepo);
      Assert.AreEqual(".pdf", metadataItemStub.ContentFileExtension);
      Assert.AreEqual(documentId + "_content", metadataItemStub.ContentFilename);
      Assert.AreEqual(documentId + "_metadata", metadataItemStub.MetadataFilename);
      Assert.AreEqual(documentId.ToString(), metadataItemStub.DocumentId.ToString());
    }



    [Test]
    public void LoadMetaData_GetAllFiles_ReturnTrue()
    {
      //arrange
      DirectoryInfo[] directoryList = new DirectoryInfo[1];

      string[] list = new string[1];
      list[0] = "Folder1";

      var directoryServiceStub = A.Fake<IDirectoryService>();
      A.CallTo(() => directoryServiceStub.GetSubFolder(@"C:\Temp\sourcePath\bla.pdf")).Returns(directoryList);
      A.CallTo(() => directoryServiceStub.GetFiles(@"C:\Temp\sourcePath\bla.pdf", @"*_Metadata.xml")).Returns(list);

   

      var stubMetaDataItemFactory = new IMetaDataItemFactory();
      var metadataItemStub = stubMetaDataItemFactory.GetMetadataItem();
      var metadataList = new List<IMetadataItem>();
      metadataList.Add(metadataItemStub);

      var guidGeneratorStub = A.Fake<IGuidGeneratorService>();
      A.CallTo(() => guidGeneratorStub.GetNewGuid()).Returns(metadataItemStub.DocumentId);

      var metadatastringList = new List<string>();
      metadatastringList.Add("metadata");

      var xmlServiceStub = A.Fake<IXmlService>();
      A.CallTo(() => xmlServiceStub.XmlToMetadataItems(metadatastringList)).Returns(metadataList);

      var filenameGeneratorServiceStub = A.Fake<IFilenameGeneratorService>();
      var sut = new  FileSystemService(xmlServiceStub, filenameGeneratorServiceStub, directoryServiceStub, guidGeneratorStub);

      //act
      var sutResult = sut.LoadMetadata();

      //assert
      Assert.IsNotNull(sutResult[0].Bezeichnung);
      Assert.IsNotNull(sutResult[0].ContentFilename);
      Assert.IsNotNull(sutResult[0].MetadataFilename);
      Assert.IsNotNull(sutResult[0].ContentFileExtension);
      Assert.IsNotNull(sutResult[0].OrginalPath);
      Assert.IsNotNull(sutResult[0].PathInRepo);
      Assert.IsNotNull(sutResult[0].Stichwoerter);
      Assert.IsNotNull(sutResult[0].Typ);
      Assert.IsNotNull(sutResult[0].ValutaYear);
      Assert.IsNotNull(sutResult[0].DocumentId.ToString());
      Assert.IsNotNull(sutResult[0].ValutaDatum.ToString());

    }
  }
}
