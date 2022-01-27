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
using Archi.Library.Models;

namespace Archi.api.Controllers
{
    public class CustomersController : BaseController<ArchiDbContext, Customer>
    {
        public CustomersController(ArchiDbContext context) : base(context)
        {
        }

        public Task TestGetAll(Settings settings)
        {
            throw new NotImplementedException();
        }
    }
}
