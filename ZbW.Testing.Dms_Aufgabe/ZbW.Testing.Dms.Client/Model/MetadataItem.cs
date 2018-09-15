using System;
using ZbW.Testing.Dms.Client.ViewModels;

namespace ZbW.Testing.Dms.Client.Model
{
  public class MetadataItem : IMetadataItem
  {
    public MetadataItem()
    {
    }

    public MetadataItem(DocumentDetailViewModel documentDetailViewModel)
    {
      if (documentDetailViewModel.ValutaDatum != null)
      {
        ValutaDatum = documentDetailViewModel.ValutaDatum.GetValueOrDefault();
        ValutaYear = ValutaDatum.Year.ToString();
        Bezeichnung = documentDetailViewModel.Bezeichnung;
        Typ = documentDetailViewModel.SelectedTypItem;
        Stichwoerter = documentDetailViewModel.Stichwoerter;
      }
    }

    public string ContentFileExtension { get; set; }
    public string ContentFilename { get; set; }
    public string OrginalPath { get; set; }
    public string MetadataFilename { get; set; }
    public Guid DocumentId { get; set; }
    public DateTime ValutaDatum { get; set; }
    public string ValutaYear { get; set; }
    public string Typ { get; set; }
    public string Bezeichnung { get; set; }
    public string Stichwoerter { get; set; }
    public string PathInRepo { get; set; }
  }
}