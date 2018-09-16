using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FakeItEasy.Core;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.Test.StubFactory
{
  public class IMetaDataItemFactory 
  {
 
    public IMetadataItem GetImMetadataItem()
    {
      var stubMetadataItem = A.Fake<IMetadataItem>();
      stubMetadataItem.Bezeichnung = "Swisscom";
      stubMetadataItem.Typ = "Vertrag";
      stubMetadataItem.ContentFileExtension = ".pdf";
      stubMetadataItem.ContentFilename = "_content.pdf";
      stubMetadataItem.MetadataFilename = "_metadata.xml";
      stubMetadataItem.DocumentId = Guid.NewGuid();
      stubMetadataItem.OrginalPath = @"C:\Temp\OrginalPath";
      stubMetadataItem.PathInRepo = @"C:\Temp\Repo";
      stubMetadataItem.Stichwoerter = "Swisscom Vertrag";
      stubMetadataItem.ValutaDatum = new DateTime(2018, 08,08);
      stubMetadataItem.ValutaYear = stubMetadataItem.ValutaDatum.Year.ToString();
      return stubMetadataItem;
    }


   
  }
}
