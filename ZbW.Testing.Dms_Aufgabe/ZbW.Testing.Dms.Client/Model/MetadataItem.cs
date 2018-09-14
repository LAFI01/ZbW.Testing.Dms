using System;
using ZbW.Testing.Dms.Client.ViewModels;

namespace ZbW.Testing.Dms.Client.Model
{
    public class MetadataItem
    {
    private DocumentDetailViewModel DocumentDetailViewModel { get; set; }

      public MetadataItem()
      {

      }

      public MetadataItem(DocumentDetailViewModel documentDetailViewModel)
      {
        DocumentDetailViewModel = documentDetailViewModel;
        if (documentDetailViewModel.ValutaDatum != null)
        {
          ValutaDatum = documentDetailViewModel.ValutaDatum.GetValueOrDefault();
        }
      }

    public string ContentFileExtension { get; set; }
      public string OrginalPath { get; set; }
      public string ContentFilename { get; set; }
      public string MetadataFilename { get; set; }
      public Guid DocumentId { get; set; }
      public DateTime ValutaDatum { get; set; }
      public string RepoYear { get; set; }
  }
}