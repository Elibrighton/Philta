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
        }

        private void OnPasteButtonCommand(object param)
        {
            FilePathTextBox = Clipboard.GetText();
        }

        private string GetSelectedDirectory()
        {
            var selectedDirectory = string.Empty;
            var directoryListBoxItem = DirectoryListBoxItemSource.Where(x => x.Id == SelectedDirectoryListBoxId).FirstOrDefault();
            
            if (directoryListBoxItem != null)
            {
                selectedDirectory = Path.Combine(@"C:\Philta", directoryListBoxItem.Name);
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
