using Microsoft.AspNetCore.Http;

namespace Archi.Library.Models
{
    public class Settings
    {
     
        public string Asc { get; set; }
        public string Desc { get; set; }
        public string Range { get; set; }
        public string Rel { get; set; }

        public bool HasOrderby()
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
