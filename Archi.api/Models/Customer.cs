using Archi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Archi.api.Models
{
    public class Customer : BaseModel
    {
        //[Table("nom de la table")]
        //[key]
        //[Column("nom colonne")]
        //[Required]
        //public int ID { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
