using System;
using System.Collections.Generic;
using System.Text;

namespace Philta.Models
{
    public class PhiltaModel :IPhiltaModel
    {
        public string FilePathTextBox { get; set; }
        public string DestinationDirectoryTextBox { get; set; }
        public string StatusLabel { get; set; }

        public PhiltaModel()
        {
            DestinationDirectoryTextBox = @"C:\Philta\";
        }
    }
}
