using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.Services
{
  public interface IGuidGeneratorService
  {
    Guid GetNewGuid();
  }
}
