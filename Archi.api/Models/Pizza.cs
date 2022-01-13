using Archi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Archi.api.Models
{
    public class Pizza : BaseModel
    {
        //public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Topping { get; set; }
    }
}
