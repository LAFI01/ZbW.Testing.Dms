using System;

namespace ZbW.Testing.Dms.Client.Model
{
  public interface IMetadataItem
  {
    string ContentFileExtension { get; set; }
    string OrginalPath { get; set; }
    string ContentFilename { get; set; }
    string MetadataFilename { get; set; }
    Guid DocumentId { get; set; }
    DateTime ValutaDatum { get; set; }
    string ValutaYear { get; set; }

    string Typ { get; set; }
    string Bezeichnung { get; set; }
    string Stichwoerter { get; set; }
    string PathInRepo { get; set; }
  }
}