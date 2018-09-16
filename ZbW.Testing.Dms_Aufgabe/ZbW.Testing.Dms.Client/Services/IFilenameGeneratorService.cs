using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.Services
{
  public interface IFilenameGeneratorService
  {
    string GetMetadataFilename(Guid guid);

    string GetContentFilename(Guid guid, string extension);
  }
}
