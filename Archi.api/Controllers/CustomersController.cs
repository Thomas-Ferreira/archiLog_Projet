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
    public class CustomersController : BaseController<ArchiDbContext, Customer>
    {
        public CustomersController(ArchiDbContext context):base(context)
        {
        }

        // GET: api/[Controller]
        /*[HttpGet("{sort}")]
        public async Task<ActionResult<IEnumerable<TModel>>> GetByOrder(string sort)
        {
            return await _context.Set<TModel>().Where(x => x.Active == true).ToListAsync();
        }*/

    }
}
