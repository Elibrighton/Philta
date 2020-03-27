using System;
using System.Collections.Generic;
using System.Text;

namespace Philta.Models
{
    public class PhiltaModel :IPhiltaModel
    {
        public string TestLabel { get; set; }

        public PhiltaModel()
        {
            TestLabel = "Test text";
        }
    }
}
