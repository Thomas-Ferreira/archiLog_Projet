using System;
using System.Collections.Generic;
using System.Text;

namespace Archi.Library.Models
{
    public class Params
    {
        public string Asc { get; set; }
        public string Desc { get; set; }
        public bool HasOrderby()
        {
            return !string.IsNullOrWhiteSpace(Asc) || !string.IsNullOrWhiteSpace(Desc);
        }
    }
}
