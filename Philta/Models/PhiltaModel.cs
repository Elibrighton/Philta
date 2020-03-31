using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Philta.Models
{
    public class PhiltaModel : IPhiltaModel
    {

        public const string RootDirectory = @"C:\Philta\";

        public string FilePathTextBox { get; set; }
        public string StatusLabel { get; set; }
        public ObservableCollection<IGenre> DirectoryListBoxItemSource { get; set; }
        public int SelectedDirectoryListBoxId { get; set; }
        public string AddDirectoryTextBox { get; set; }
        public bool IsSongTypeRemixChecked { get; set; }
        public bool IsSongTypeOriginalChecked { get; set; }

        public PhiltaModel()
        {
            DirectoryListBoxItemSource = new ObservableCollection<IGenre>();
            SetDirectoryListBoxItemSource(RootDirectory);
            SelectedDirectoryListBoxId = -1;
            IsSongTypeRemixChecked = true;
        }

        public void SetDirectoryListBoxItemSource(string rootDirectory = RootDirectory)
        {
            var subDirectories = Directory.GetDirectories(rootDirectory);

            for (int i = 0; i < subDirectories.Length; i++)
            {
                var directoryName = new DirectoryInfo(subDirectories[i]).Name;

                if (directoryName == "Remix" || directoryName == "Original")
                {
                    SetDirectoryListBoxItemSource(Path.Combine(rootDirectory, directoryName));
                }
                else
                {
                    if (DirectoryListBoxItemSource.Where(x => x.Name == directoryName).FirstOrDefault() == null)
                    {
                        var id = DirectoryListBoxItemSource.Count;
                        DirectoryListBoxItemSource.Add(new Genre { Id = id, Name = directoryName });
                    }
                }
            }

            DirectoryListBoxItemSource = new ObservableCollection<IGenre>(DirectoryListBoxItemSource.OrderBy(x => x.Name));
        }
    }
}
