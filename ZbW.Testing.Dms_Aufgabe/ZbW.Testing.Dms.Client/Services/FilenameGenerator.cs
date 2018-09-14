using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.Services
{
  public class FilenameGenerator
  {
    public string GetContentFilename(Guid guid, string extension)
    {
      return guid + "_Content" + extension;
    }

    public string GetMetadataFilename(Guid guid)
    {
      return guid + "_Metadata" + ".xml";
    }
  }
}
