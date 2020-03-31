using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Philta.Models
{
    public interface IPhiltaModel
    {
        public const string RootDirectory = @"C:\Philta\";
        string FilePathTextBox { get; set; }
        string StatusLabel { get; set; }
        ObservableCollection<IGenre> DirectoryListBoxItemSource { get; set; }
        int SelectedDirectoryListBoxId { get; set; }
        string AddDirectoryTextBox { get; set; }
        void SetDirectoryListBoxItemSource(string rootDirectory = RootDirectory);
        bool IsSongTypeRemixChecked { get; set; }
        bool IsSongTypeOriginalChecked { get; set; }
    }
}
