using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services.Impl
{
  public class FileSystemService
  {
    private const  string TargetPath = @"C:\Temp\DMS";
    private static readonly IList<string> DirectoryFolder = new List<string>();

    public FileSystemService()
    {
      XmlService = new XmlService();
      FilenameGenerator = new FilenameGenerator();
    }

    public FileSystemService(IXmlService xmlService, IFilenameGenerator filenameGenerator,
      IList<string> directoryFolder)
    {
      XmlService = xmlService;
      FilenameGenerator = filenameGenerator;
    }

    private IXmlService XmlService { get; }
    private IFilenameGenerator FilenameGenerator { get; }

    private IMetadataItem MetaDataIteam { get; set; }

    public void AddFile(IMetadataItem metadataItem, bool isRemoveFileEnabled, string sourcePath)
    {
      MetaDataIteam = metadataItem;

      var documentId = Guid.NewGuid();
      var extension = Path.GetExtension(sourcePath);
      var contentFileName = FilenameGenerator.GetContentFilename(documentId, extension);
      var metadataFilename = FilenameGenerator.GetMetadataFilename(documentId);

      var targetDir = Path.Combine(TargetPath, MetaDataIteam.ValutaYear);

      MetaDataIteam.OrginalPath = sourcePath;
      MetaDataIteam.PathInRepo = targetDir + @"\" + contentFileName;
      MetaDataIteam.ContentFileExtension = extension;
      MetaDataIteam.ContentFilename = contentFileName;
      MetaDataIteam.MetadataFilename = metadataFilename;
      MetaDataIteam.DocumentId = documentId;

      CreateDirectoryFolder(targetDir);

      XmlService.MetadataItemToXml(MetaDataIteam, targetDir);
      DeleteFile(MetaDataIteam, isRemoveFileEnabled);
    }

    public IList<IMetadataItem> LoadMetadata()
    {
      var metadataFile = GetAllFiles();
      var metadataList = XmlService.XmlToMetadataItems(metadataFile);
      return metadataList;
    }

    private IList<string> GetAllFiles()
    {
      var metadataFile = new List<string>();
      foreach (var d in DirectoryFolder)
      {
        var path = TargetPath + @"\" + d + @"\*_Metadata.xml";
        var list = Directory.GetFiles(TargetPath + @"\" + d, @"*_Metadata.xml");
        metadataFile.AddRange(list);
      }

      return metadataFile;
    }

    private void CreateDirectoryFolder(string targetDir)
    {
      if (!Directory.Exists(targetDir))
      {
        Directory.CreateDirectory(targetDir);
        DirectoryFolder.Add(MetaDataIteam.ValutaYear);
      }
    }

    private void DeleteFile(IMetadataItem metadataItem, bool deleteFile)
    {
      if (deleteFile)
      {
        var task = Task.Factory.StartNew(() =>
        {
          Task.Delay(500);
          File.Delete(metadataItem.OrginalPath);
        });
        try
        {
          Task.WaitAll(task);
        }
        catch (Exception e)
        {
          MessageBox.Show(e.Message);
        }
      }
    }
  }
}