using System;
using System.Collections.Generic;
using System.Text;

namespace Archi.Library.Models
{
    public class Settings
    {
     
        public string Asc { get; set; }
        public string Desc { get; set; }
        public string Range { get; set; }
        public string Rel { get; set; }

        public bool HasOrder()
        {
            return !string.IsNullOrWhiteSpace(Asc) || !string.IsNullOrWhiteSpace(Desc);
        }
        public bool HasRange()
        {
            return !string.IsNullOrWhiteSpace(Range);
        }
        public bool HasRel()
        {
            return !string.IsNullOrWhiteSpace(Rel);
        }
     
    }
}
