using System;
using System.Collections.Generic;
using System.Text;

namespace Archi.Library.Models
{
    public class Settings
    {
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Rating { get; set; }
        public string asc { get; set; }
        public string desc { get; set; }
        public string Filter { get; set; }
        public bool HasOrder()
        {
            return !string.IsNullOrWhiteSpace(asc) || !string.IsNullOrWhiteSpace(desc);
        }
        public bool HasFilter()
        {
            return !string.IsNullOrWhiteSpace(Filter);
        }
    }
}