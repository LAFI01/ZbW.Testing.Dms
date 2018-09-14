using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
  public interface IFileSystemService
  {

    void AddFile(MetadataItem metadataItem, bool deleteFile, string sourcePath);
  }
}
