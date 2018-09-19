using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows.Controls;
using Prism.Commands;
using Prism.Mvvm;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Repositories;
using ZbW.Testing.Dms.Client.Services;
using ZbW.Testing.Dms.Client.Services.Impl;

namespace ZbW.Testing.Dms.Client.ViewModels
{
  internal class SearchViewModel : BindableBase
  {
    private FileSystemService _fileSystemService;
    private ObservableCollection<IMetadataItem> _filteredMetadataItems;
    private IMetadataItem _selectedMetadataItem;

    private string _selectedTypItem;

    private string _suchbegriff;

    private List<string> _typItems;

    public SearchViewModel()
    {
      TypItems = ComboBoxItems.Typ;

      CmdSuchen = new DelegateCommand(OnCmdSuchen, OnCanCmdSuchen);
      CmdReset = new DelegateCommand(OnCmdReset);
      CmdOeffnen = new DelegateCommand(OnCmdOeffnen, OnCanCmdOeffnen);
      _fileSystemService = new FileSystemService();
      _filteredMetadataItems = new ObservableCollection<IMetadataItem>();
      ShowData();
      
    }

    public DelegateCommand CmdOeffnen { get; }

    public DelegateCommand CmdSuchen { get; }

    public DelegateCommand CmdReset { get; }


    public string Suchbegriff
    {
      get { return _suchbegriff; }

      set { if(SetProperty(ref _suchbegriff, value)) {
          CmdSuchen.RaiseCanExecuteChanged();
        }
      }
    }

    public List<string> TypItems
    {
      get { return _typItems; }

      set { SetProperty(ref _typItems, value); }
    }

    public string SelectedTypItem
    {
      get { return _selectedTypItem; }

      set
      {
        if(SetProperty(ref _selectedTypItem, value))
        {
          CmdSuchen.RaiseCanExecuteChanged();
        }
      }
    }

    public ObservableCollection<IMetadataItem> FilteredMetadataItems
    {
      get { return _filteredMetadataItems; }

      set
      {
        SetProperty(ref _filteredMetadataItems, value);

        RaisePropertyChanged("FilteredMetadataItems");
      }
    }

    public IMetadataItem SelectedMetadataItem
    {
      get { return _selectedMetadataItem; }

      set
      {
        if (SetProperty(ref _selectedMetadataItem, value))
        {
          CmdOeffnen.RaiseCanExecuteChanged();
        }
      }
    }

    private void ShowData()
    {
      _fileSystemService = new FileSystemService();
      var metadataList = _fileSystemService.LoadMetadata();
      foreach (var m in metadataList)
      {
        FilteredMetadataItems.Add(m);
      }
    }

    public bool OnCanCmdOeffnen()
    {
      return SelectedMetadataItem != null;
    }

    private void OnCmdOeffnen()
    {
      System.Diagnostics.Process process = new System.Diagnostics.Process();
      process.EnableRaisingEvents = false;
      process.StartInfo.FileName = SelectedMetadataItem.PathInRepo;
      process.Start();
    }

    public bool OnCanCmdSuchen()
    {
      return (Suchbegriff != null && SelectedTypItem != null);
    }

    public void OnCmdSuchen()
    {
      var tempList = new List<IMetadataItem>();
      foreach (var m in FilteredMetadataItems)
      {
        if (m.Stichwoerter != null)
        {
          if (m.Bezeichnung.ToLower().Equals(Suchbegriff.ToLower()) ||
              m.Stichwoerter.ToLower().Equals(Suchbegriff.ToLower()) || m.Typ.Equals(SelectedTypItem))
          {
            tempList.Add(m);
          }
        }
        else
        {
          if (m.Bezeichnung.ToLower().Equals(Suchbegriff.ToLower()) ||
               m.Typ.Equals(SelectedTypItem))
          {
            tempList.Add(m);
          }
        }
      }
      FilteredMetadataItems.Clear();
      FilteredMetadataItems.AddRange(tempList);
    }

    public void OnCmdReset()
    {
      FilteredMetadataItems.Clear();
      Suchbegriff = null;
      SelectedTypItem = null;
      ShowData();
    }
  }
}