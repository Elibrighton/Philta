using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Philta.Models
{
    public interface IPhiltaModel
    {
        string FilePathTextBox { get; set; }
        string StatusLabel { get; set; }
        ObservableCollection<IGenre> DirectoryListBoxItemSource { get; set; }
        int SelectedDirectoryListBoxId { get; set; }
        string AddDirectoryTextBox { get; set; }
        ObservableCollection<IGenre> GetDirectoryListBoxItemSource();
    }
}
