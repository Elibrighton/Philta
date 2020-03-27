using System;
using System.Collections.Generic;
using System.Text;

namespace Philta.Models
{
    public interface IPhiltaModel
    {
        string FilePathTextBox { get; set; }
        string DestinationDirectoryTextBox { get; set; }
        string StatusLabel { get; set; }
    }
}
