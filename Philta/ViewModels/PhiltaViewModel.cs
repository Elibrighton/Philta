using Philta.Base;
using Philta.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Philta.ViewModels
{
    public class PhiltaViewModel : ObservableObject, IPhiltaViewModel
    {
        public ICommand CopyButtonCommand { get; set; }
        public ICommand ClearButtonCommand { get; set; }
        public ICommand PasteButtonCommand { get; set; }
        public ICommand AddDirectoryButtonCommand { get; set; }

        private readonly IPhiltaModel _philtaModel;

        public PhiltaViewModel(IPhiltaModel philtaModel)
        {
            _philtaModel = philtaModel;

            CopyButtonCommand = new RelayCommand(OnCopyButtonCommand);
            ClearButtonCommand = new RelayCommand(OnClearButtonCommand);
            PasteButtonCommand = new RelayCommand(OnPasteButtonCommand);
            AddDirectoryButtonCommand = new RelayCommand(OnAddDirectoryButtonCommand);
        }

        public string FilePathTextBox
        {
            get { return _philtaModel.FilePathTextBox; }
            set
            {
                _philtaModel.FilePathTextBox = value;
                NotifyPropertyChanged("FilePathTextBox");
            }
        }

        public string StatusLabel
        {
            get { return _philtaModel.StatusLabel; }
            set
            {
                _philtaModel.StatusLabel = value;
                NotifyPropertyChanged("StatusLabel");
            }
        }

        public ObservableCollection<IGenre> DirectoryListBoxItemSource
        {
            get { return _philtaModel.DirectoryListBoxItemSource; }
            set
            {
                _philtaModel.DirectoryListBoxItemSource = value;
                NotifyPropertyChanged("DirectoryListBoxItemSource");
            }
        }

        public int SelectedDirectoryListBoxId
        {
            get { return _philtaModel.SelectedDirectoryListBoxId; }
            set
            {
                _philtaModel.SelectedDirectoryListBoxId = value;
                NotifyPropertyChanged("SelectedDirectoryListBoxId");
            }
        }

        public string AddDirectoryTextBox
        {
            get { return _philtaModel.AddDirectoryTextBox; }
            set
            {
                _philtaModel.AddDirectoryTextBox = value;
                NotifyPropertyChanged("AddDirectoryTextBox");
            }
        }

        public bool IsSongTypeRemixChecked
        {
            get { return _philtaModel.IsSongTypeRemixChecked; }
            set
            {
                _philtaModel.IsSongTypeRemixChecked = value;
                NotifyPropertyChanged("IsSongTypeRemixChecked");
            }
        }

        public bool IsSongTypeOriginalChecked
        {
            get { return _philtaModel.IsSongTypeOriginalChecked; }
            set
            {
                _philtaModel.IsSongTypeOriginalChecked = value;
                NotifyPropertyChanged("IsSongTypeOriginalChecked");
            }
        }

        private void OnCopyButtonCommand(object param)
        {
            StatusLabel = "";
            var validationError = GetValidationError();

            if (!string.IsNullOrEmpty(validationError))
            {
                StatusLabel = validationError;
            }
            else
            {
                var fileName = Path.GetFileName(FilePathTextBox);
                var selectedDirectory = GetSelectedDirectory();

                if (!Directory.Exists(selectedDirectory))
                {
                    Directory.CreateDirectory(selectedDirectory);
                }

                var destinationPath = Path.Combine(selectedDirectory, fileName);

                File.Copy(FilePathTextBox, destinationPath);

                if (File.Exists(destinationPath))
                {
                    StatusLabel += "File copied";
                }
                else
                {
                    StatusLabel += "Copy failed";
                }
            }
        }

        private void OnClearButtonCommand(object param)
        {
            FilePathTextBox = "";
            SelectedDirectoryListBoxId = -1;
            StatusLabel = "";
            AddDirectoryTextBox = "";
            IsSongTypeRemixChecked = true;
            IsSongTypeOriginalChecked = false;
        }

        private void OnPasteButtonCommand(object param)
        {
            FilePathTextBox = Clipboard.GetText();
        }

        private void OnAddDirectoryButtonCommand(object param)
        {
            var newDirectory = GetNewDirectory();

            if (!Directory.Exists(newDirectory))
            {
                Directory.CreateDirectory(newDirectory);

                if (Directory.Exists(newDirectory))
                {
                    AddDirectoryTextBox = "";
                    StatusLabel = "Directory created";
                    DirectoryListBoxItemSource = new ObservableCollection<IGenre>();
                    _philtaModel.SetDirectoryListBoxItemSource();
                    DirectoryListBoxItemSource = _philtaModel.DirectoryListBoxItemSource;
                }
            }
            else
            {
                StatusLabel = "Directory already exists";
            }
        }

        private string GetNewDirectory()
        {
            string songType = GetSongType();

            return Path.Combine(IPhiltaModel.RootDirectory, songType, AddDirectoryTextBox);
        }

        private string GetSongType()
        {
            var songType = string.Empty;

            if (IsSongTypeRemixChecked)
            {
                songType = "Remix";
            }
            else
            {
                songType = "Original";
            }

            return songType;
        }

        private string GetSelectedDirectory()
        {
            var selectedDirectory = string.Empty;
            var directoryListBoxItem = DirectoryListBoxItemSource.Where(x => x.Id == SelectedDirectoryListBoxId).FirstOrDefault();
            
            if (directoryListBoxItem != null)
            {
                string songType = GetSongType();
                selectedDirectory = Path.Combine(@"C:\Philta", songType, directoryListBoxItem.Name);
            }

            return selectedDirectory;
        }

        private string GetValidationError()
        {
            var validationError = string.Empty;

            if (string.IsNullOrEmpty(FilePathTextBox))
            {
                validationError = "File path is empty";
            }
            else if (!File.Exists(FilePathTextBox))
            {
                validationError = "File does not exists";
            }
            else if (SelectedDirectoryListBoxId == -1)
            {
                validationError = "Desitnation directory is not selected";
            }

            return validationError;
        }
    }
}
