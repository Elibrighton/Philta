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

        public const string RootDirectory = @"C:\Philta music\";

        public string FilePathTextBox { get; set; }
        public string StatusLabel { get; set; }
        public bool IsSongTypeRemixChecked { get; set; }
        public bool IsSongTypeOriginalChecked { get; set; }
        public ObservableCollection<IGenre> DirectoryComboBoxItemSource { get; set; }
        public IGenre SelectedDirectoryComboBoxItem { get; set; }

        public string DirectoryText { get; set; }

        public PhiltaModel()
        {
            IsSongTypeRemixChecked = true;
            DirectoryComboBoxItemSource = new ObservableCollection<IGenre>();
            SetDirectoryComboBoxItemSource(RootDirectory);
        }

        public void SetDirectoryComboBoxItemSource(string rootDirectory = RootDirectory)
        {
            var subDirectories = Directory.GetDirectories(rootDirectory);

            for (int i = 0; i < subDirectories.Length; i++)
            {
                var directoryName = new DirectoryInfo(subDirectories[i]).Name;

                if (directoryName == "Remix" || directoryName == "Original")
                {
                    SetDirectoryComboBoxItemSource(Path.Combine(rootDirectory, directoryName));
                }
                else
                {
                    if (DirectoryComboBoxItemSource.Where(x => x.Name == directoryName).FirstOrDefault() == null)
                    {
                        var id = DirectoryComboBoxItemSource.Count;
                        DirectoryComboBoxItemSource.Add(new Genre { Id = id, Name = directoryName });
                    }
                }
            }

            DirectoryComboBoxItemSource = new ObservableCollection<IGenre>(DirectoryComboBoxItemSource.OrderBy(x => x.Name));
        }
    }
}
