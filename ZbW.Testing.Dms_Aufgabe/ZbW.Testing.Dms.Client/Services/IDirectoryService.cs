using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
 public  interface IDirectoryService
 {
   string Combine(string targetPath, string valutaYear);
    string GetExtension(string sourcePath);

   void CreateDirectoryFolder(string targetDir);

   void DeleteFile(IMetadataItem metadataItem, bool deleteFile);

   string[] GetFiles(string targetPath, string pathPattern);
   DirectoryInfo[] GetSubFolder(string targetPath);
 }
}
