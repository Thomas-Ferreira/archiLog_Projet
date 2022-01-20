using System;
using System.Collections.Generic;
using System.Text;

namespace Archi.Library.Models
{
    public class Filtre
    {
        public string Type { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Rating { get; set; }
        public string Asc { get; set; }
        public string Desc { get; set; }
        public string Range { get; set; }

        public string sortOrder { get; set; }
        public string searchString { get; private set; }

        public bool HasOrder()
        {
            return !string.IsNullOrWhiteSpace(Asc) || !string.IsNullOrWhiteSpace(Desc);
        }
        public bool HasRange()
        {
            return !string.IsNullOrWhiteSpace(Range);
        }
    }
}