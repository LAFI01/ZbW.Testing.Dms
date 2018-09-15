using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
  public class FileSystemService : IFileSystemService
  {
    private const string TargetPath = @"C:\Temp\DMS";
    private readonly FilenameGenerator _filenameGenerator = new FilenameGenerator();

    private string SourcePath { get; set; }

    public void AddFile(MetadataItem metadataItem, bool deleteFile, string sourcePath)
    {
      SourcePath = sourcePath;
      var year = metadataItem.ValutaDatum.Year;
      var documentId = Guid.NewGuid();
      var extension = Path.GetExtension(SourcePath);
      var contentFileName = _filenameGenerator.GetContentFilename(documentId, extension);
      var metadataFilename = _filenameGenerator.GetMetadataFilename(documentId);

      var targetDir = Path.Combine(TargetPath, year.ToString());

      metadataItem.OrginalPath = sourcePath;
      metadataItem.ContentFileExtension = extension;
      metadataItem.ContentFilename = contentFileName;
      metadataItem.MetadataFilename = metadataFilename;
      metadataItem.DocumentId = documentId;
      metadataItem.RepoYear = year.ToString();
      metadataItem.PathInRepo = targetDir + @"\" + contentFileName;


      var xmlSerializer = new XmlSerializer(typeof(MetadataItem));

      if (!Directory.Exists(targetDir))
      {
        Directory.CreateDirectory(targetDir);
      }

      var streamWriter = new StreamWriter(Path.Combine(targetDir, metadataFilename));
      xmlSerializer.Serialize(streamWriter, metadataItem);
      streamWriter.Flush();
      streamWriter.Close();

      File.Copy(metadataItem.OrginalPath, Path.Combine(targetDir, contentFileName));
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

    public List<MetadataItem> LoadMetadata()
    {
      var metadataFile = Directory.GetFiles(TargetPath + @"\2018", "*_Metadata.xml");
      List<MetadataItem> meta = new List<MetadataItem>();
      foreach (var mf in metadataFile)
      {
        var xmlSerializer = new XmlSerializer(typeof(MetadataItem));
        var streamReader = new StreamReader(mf);
         var m = (MetadataItem) xmlSerializer.Deserialize(streamReader);
       meta.Add(m);
      }

      return meta;
    }
  }
}