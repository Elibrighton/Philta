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

        private readonly IPhiltaModel _philtaModel;

        public PhiltaViewModel(IPhiltaModel philtaModel)
        {
            _philtaModel = philtaModel;

            CopyButtonCommand = new RelayCommand(OnCopyButtonCommand);
            ClearButtonCommand = new RelayCommand(OnClearButtonCommand);
            PasteButtonCommand = new RelayCommand(OnPasteButtonCommand);
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

        public ObservableCollection<IGenre> DirectoryComboBoxItemSource
        {
            get { return _philtaModel.DirectoryComboBoxItemSource; }
            set
            {
                _philtaModel.DirectoryComboBoxItemSource = value;
                NotifyPropertyChanged("DirectoryComboBoxItemSource");
            }
        }

        public IGenre SelectedDirectoryComboBoxItem
        {
            get { return _philtaModel.SelectedDirectoryComboBoxItem; }
            set
            {
                _philtaModel.SelectedDirectoryComboBoxItem = value;
                NotifyPropertyChanged("SelectedDirectoryComboBoxItem");
            }
        }

        public string DirectoryText
        {
            get { return _philtaModel.DirectoryText; }
            set
            {
                _philtaModel.DirectoryText = value;

                NotifyPropertyChanged("DirectoryText");
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

                if (!File.Exists(destinationPath))
                {
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
                else
                {
                    StatusLabel += "File already exists";
                }
            }
        }

        private void OnClearButtonCommand(object param)
        {
            FilePathTextBox = "";
            StatusLabel = "";
            IsSongTypeRemixChecked = true;
            IsSongTypeOriginalChecked = false;
            SelectedDirectoryComboBoxItem = null;

            if (!string.IsNullOrEmpty(DirectoryText))
            {
                DirectoryComboBoxItemSource = new ObservableCollection<IGenre>();
                _philtaModel.SetDirectoryComboBoxItemSource();
                DirectoryComboBoxItemSource = _philtaModel.DirectoryComboBoxItemSource;
            }

            DirectoryText = "";
        }

        private void OnPasteButtonCommand(object param)
        {
            FilePathTextBox = Clipboard.GetText();
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
            string songType = GetSongType();
            var destinationDirectory = SelectedDirectoryComboBoxItem == null ? DirectoryText : SelectedDirectoryComboBoxItem.Name;
            
            return Path.Combine(@"C:\Philta", songType, destinationDirectory);
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
            else if (SelectedDirectoryComboBoxItem == null && string.IsNullOrEmpty(DirectoryText))
            {
                validationError = "Destination directory is not selected";
            }

            return validationError;
        }
    }
}
