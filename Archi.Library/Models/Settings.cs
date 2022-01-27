using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Text;


namespace Archi.Library.Models
{
    public class Settings
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedTime { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
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