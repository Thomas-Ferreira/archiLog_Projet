using Microsoft.AspNetCore.Http;
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

        public bool isAsc(QueryString queryString)
        {
            if (queryString.ToString().IndexOf("asc", 0) == -1)
            {
                return false;
            }
            if (queryString.ToString().IndexOf("desc", 0) == -1)
            {
                return true;
            }
            if (queryString.ToString().IndexOf("asc", 0) < queryString.ToString().IndexOf("desc", 0))
            {
                return true;
            }

            return false;
           
        }
    }
}
