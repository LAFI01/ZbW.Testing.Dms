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

    public FileSystemService()
    {
      XmlService = new XmlService();
      FilenameGeneratorService = new FilenameGeneratorServiceService();
      DirectoryService = new DirectoryService();
    }

    public FileSystemService(IXmlService xmlService, IFilenameGeneratorService filenameGeneratorService, IDirectoryService directoryService)
    {
      XmlService = xmlService;
      FilenameGeneratorService = filenameGeneratorService;
      DirectoryService = directoryService;
    }

    private IXmlService XmlService { get; }
    private IFilenameGeneratorService FilenameGeneratorService { get; }
    private IDirectoryService DirectoryService { get; }
    private IMetadataItem MetaDataIteam { get; set; }

    public void AddFile(IMetadataItem metadataItem, bool isRemoveFileEnabled, string sourcePath)
    {
      MetaDataIteam = metadataItem;

      var documentId = Guid.NewGuid();
      var extension = DirectoryService.GetExtension(sourcePath);
      MetaDataIteam.ContentFilename = FilenameGeneratorService.GetContentFilename(documentId, extension);
      MetaDataIteam.MetadataFilename = FilenameGeneratorService.GetMetadataFilename(documentId);

      var targetDir = DirectoryService.Combine(TargetPath, MetaDataIteam.ValutaYear);

      MetaDataIteam.OrginalPath = sourcePath;
      MetaDataIteam.PathInRepo = targetDir + @"\" + MetaDataIteam.ContentFilename;
      MetaDataIteam.ContentFileExtension = extension;
      MetaDataIteam.ContentFilename = MetaDataIteam.ContentFilename;
      MetaDataIteam.DocumentId = documentId;

      DirectoryService.CreateDirectoryFolder(targetDir);

      XmlService.MetadataItemToXml(MetaDataIteam, targetDir);
      DirectoryService.DeleteFile(MetaDataIteam, isRemoveFileEnabled);
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
      var nameOfAllSubFolder = GetAllSubFolder();
      foreach (var d in nameOfAllSubFolder)
      {
     
        var list = DirectoryService.GetFiles(TargetPath + @"\" + d, @"*_Metadata.xml");
        metadataFile.AddRange(list);
      }

      return metadataFile;
    }

    private IList<string> GetAllSubFolder()
    {
      var nameOfAllSubFolder = new List<string>();
      var subFolder = DirectoryService.GetSubFolder(TargetPath);
      foreach (var n in subFolder)
      {
       nameOfAllSubFolder.Add(n.Name);
      }

      return nameOfAllSubFolder;
    }

  

    
  }
}