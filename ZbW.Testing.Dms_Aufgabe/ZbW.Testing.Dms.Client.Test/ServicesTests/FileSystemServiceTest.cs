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
    public void AddFile_CheckSuccess_ObjectAreNotNUll()
    {
      //arrange
      var stubMetaDataItemFactory = new IMetaDataItemFactory();
      var metadataItemStub = stubMetaDataItemFactory.GetImMetadataItem();
      var guidStub = Guid.NewGuid();

      var directoryServiceStub = A.Fake<IDirectoryService>();
      A.CallTo(() => directoryServiceStub.GetExtension(@"C:\Temp\sourcePath\bla.pdf")).Returns(".pdf");
      A.CallTo(() => directoryServiceStub.Combine("targetPath", "2018")).Returns(@"C:\Temp\targetPath\2018");
      A.CallTo(directoryServiceStub).WithVoidReturnType().DoesNothing();

      var filenameGeneratorServiceStub = A.Fake<IFilenameGeneratorService>();
      A.CallTo(() => filenameGeneratorServiceStub.GetContentFilename(guidStub, ".pdf")).Returns(guidStub + "_content");
      A.CallTo(() => filenameGeneratorServiceStub.GetMetadataFilename(guidStub)).Returns(guidStub + "_metadata");

      var xmlServiceStub = A.Fake<IXmlService>();
      A.CallTo(() => xmlServiceStub.MetadataItemToXml(metadataItemStub, "targetPath2018")).DoesNothing();

      var sut = new FileSystemService(xmlServiceStub, filenameGeneratorServiceStub, directoryServiceStub);

      //act
      sut.AddFile(metadataItemStub, false, "sourcePath");

      //assert
      Assert.IsNotNull(metadataItemStub.Bezeichnung);
      Assert.IsNotNull(metadataItemStub.ContentFilename);
      Assert.IsNotNull(metadataItemStub.MetadataFilename);
      Assert.IsNotNull(metadataItemStub.ContentFileExtension);
      Assert.IsNotNull(metadataItemStub.OrginalPath);
      Assert.IsNotNull(metadataItemStub.PathInRepo);
      Assert.IsNotNull(metadataItemStub.Stichwoerter);
      Assert.IsNotNull(metadataItemStub.Typ);
      Assert.IsNotNull(metadataItemStub.ValutaYear);
      Assert.IsNotNull(metadataItemStub.DocumentId.ToString());
      Assert.IsNotNull(metadataItemStub.ValutaDatum.ToString());
    }

    [Test]
    public void LoadMetaData_CkeckSuccess_ObjectAreNotNUll()
    {
      //arrange
      DirectoryInfo[] directoryList = new DirectoryInfo[1];

      string[] list = new string[1];
      list[0] = "Folder1";

      var directoryServiceStub = A.Fake<IDirectoryService>();
      A.CallTo(() => directoryServiceStub.GetSubFolder(@"C:\Temp\sourcePath\bla.pdf")).Returns(directoryList);
      A.CallTo(() => directoryServiceStub.GetFiles(@"C:\Temp\sourcePath\bla.pdf", @"*_Metadata.xml")).Returns(list);

      var stubMetaDataItemFactory = new IMetaDataItemFactory();
      var metadataItemStub = stubMetaDataItemFactory.GetImMetadataItem();
      var metadataList = new List<IMetadataItem>();
      metadataList.Add(metadataItemStub);

      var metadatastringList = new List<string>();
      metadatastringList.Add("metadata");

      var xmlServiceStub = A.Fake<IXmlService>();
      A.CallTo(() => xmlServiceStub.XmlToMetadataItems(metadatastringList)).Returns(metadataList);

      var filenameGeneratorServiceStub = A.Fake<IFilenameGeneratorService>();
      var sut = new  FileSystemService(xmlServiceStub, filenameGeneratorServiceStub, directoryServiceStub);

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
