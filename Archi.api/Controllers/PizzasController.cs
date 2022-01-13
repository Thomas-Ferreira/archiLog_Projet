using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Archi.api.Data;
using Archi.api.Models;
using Archi.Library.Controllers;

namespace Archi.api.Controllers
{
    public class PizzasController : BaseController<ArchiDbContext, Pizza>
    {
        public PizzasController(ArchiDbContext context) : base(context)
        {
            
        }
    }
}
