using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Philta.Models
{
    public class PhiltaModel : IPhiltaModel
    {
        public string FilePathTextBox { get; set; }
        public string StatusLabel { get; set; }
        public ObservableCollection<IGenre> DirectoryListBoxItemSource { get; set; }
        public int SelectedDirectoryListBoxId { get; set; }

        public PhiltaModel()
        {
            DirectoryListBoxItemSource = GetDirectoryListBoxItemSource();
            SelectedDirectoryListBoxId = -1;
        }

        private ObservableCollection<IGenre> GetDirectoryListBoxItemSource()
        {
            var rootDirectory = @"C:\Philta";
            var subDirectories = Directory.GetDirectories(rootDirectory);

            var itemSource = new ObservableCollection<IGenre>();

            for (int i = 0; i < subDirectories.Length; i++)
            {
                var directoryName = new DirectoryInfo(subDirectories[i]).Name;
                itemSource.Add(new Genre { Id = i, Name = directoryName });
            }

            return itemSource;
        }
    }
}
