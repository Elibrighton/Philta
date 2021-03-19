using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Philta.Models
{
    public interface IPhiltaModel
    {
        public const string RootDirectory = @"C:\Philta music\";
        string FilePathTextBox { get; set; }
        string StatusLabel { get; set; }
        bool IsSongTypeRemixChecked { get; set; }
        bool IsSongTypeOriginalChecked { get; set; }
        ObservableCollection<IGenre> DirectoryComboBoxItemSource { get; set; }
        IGenre SelectedDirectoryComboBoxItem { get; set; }
        string DirectoryText { get; set; }

        void SetDirectoryComboBoxItemSource(string rootDirectory = RootDirectory);
    }
}
