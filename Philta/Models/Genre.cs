using System;
using System.Collections.Generic;
using System.Text;

namespace Philta.Models
{
    public class Genre : IGenre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
