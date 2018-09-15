using System.Collections.Generic;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
  public interface IXmlService
  {
    void MetadataItemToXml(IMetadataItem metadataItem, string targetDir);

    IList<IMetadataItem> XmlToMetadataItems(IList<string> metadataFile);
  }
}