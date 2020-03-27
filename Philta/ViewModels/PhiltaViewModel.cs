using Philta.Base;
using Philta.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace Philta.ViewModels
{
    public class PhiltaViewModel : ObservableObject, IPhiltaViewModel
    {
        public ICommand CopyButtonCommand { get; set; }
        public ICommand ClearButtonCommand { get; set; }

        private readonly IPhiltaModel _philtaModel;

        public PhiltaViewModel(IPhiltaModel philtaModel)
        {
            _philtaModel = philtaModel;

            CopyButtonCommand = new RelayCommand(OnCopyButtonCommand);
            ClearButtonCommand = new RelayCommand(OnClearButtonCommand);
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

        public string DestinationDirectoryTextBox
        {
            get { return _philtaModel.DestinationDirectoryTextBox; }
            set
            {
                _philtaModel.DestinationDirectoryTextBox = value;
                NotifyPropertyChanged("DestinationDirectoryTextBox");
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

        private void OnCopyButtonCommand(object param)
        {
            var validationError = GetValidationError();

            if (!string.IsNullOrEmpty(validationError))
            {
                StatusLabel = validationError;
            }
            else
            {
                var fileName = Path.GetFileName(FilePathTextBox);

                if (!Directory.Exists(DestinationDirectoryTextBox))
                {
                    Directory.CreateDirectory(DestinationDirectoryTextBox);
                }
                var destinationPath = Path.Combine(DestinationDirectoryTextBox, fileName);

                File.Copy(FilePathTextBox, destinationPath);

                if (File.Exists(destinationPath))
                {
                    StatusLabel = "File copied";
                }
                else
                {
                    StatusLabel = "Copy failed";
                }
            }
        }

        private void OnClearButtonCommand(object param)
        {
            FilePathTextBox = "";
            DestinationDirectoryTextBox = @"C:\Philta\";
            StatusLabel = "";
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
            else if (string.IsNullOrEmpty(DestinationDirectoryTextBox))
            {
                validationError = "Destination directory is empty";
            }
            else if (!DestinationDirectoryTextBox.Contains(@"C:\Philta\"))
            {
                validationError = "Destination directory is not inside \'C:\\Philta\\\' directory";
            }

            return validationError;
        }
    }
}
