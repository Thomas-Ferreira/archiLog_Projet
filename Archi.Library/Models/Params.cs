using System;
using System.Collections.Generic;
using System.Text;

namespace Archi.Library.Models
{
    public class Params
    {
        public string asc { get; set; }
        public string desc { get; set; }
        public bool HasOrderby()
        {
            return !string.IsNullOrWhiteSpace(asc) || !string.IsNullOrWhiteSpace(desc);
        }
    }
}
