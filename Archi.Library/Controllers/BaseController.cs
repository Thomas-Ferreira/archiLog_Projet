using Archi.Library.Data;
using Archi.Library.Models;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Archi.Library.Controllers
{   
     

    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TContext, TModel> : ControllerBase where TContext : BaseDbContext where TModel : BaseModel

    {
        protected readonly TContext _context;

        public IFormatProvider LinkHeaderTemplate { get; private set; }

        public BaseController(TContext context)
        {
            _context = context;
        }

        
        // GET: api/[Controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TModel>>> GetAll([FromQuery]Settings setting)
        {
            var query = _context.Set<TModel>()
                                  .Where(x => x.Active == true);

           // this.Response.Headers.Add("Accepted Range", "12");

            if (setting.HasRange())
            {
                var tab = setting.Range.Split('-');
                var rel = setting.Rel;
              
                var pagin = new PaginatedList<TModel>(query, int.Parse(tab[0]), int.Parse(tab[1]));
                query = await pagin.PagineAsync();
                int total = _context.Set<TModel>().Count();
            }

            return await query.ToListAsync() ;
        }

        // GET: api/[Controller]/5
        [HttpGet("{id}")]
    public async Task<ActionResult<TModel>> GetById(int id)
    {
    var controller = await _context.Set<TModel>().SingleOrDefaultAsync(x => x.ID==id && x.Active);

    if (controller == null)
    {
        return NotFound();
    }

    return controller;
    }

    // POST: api/[Controller]
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<TModel>> PostController(TModel model)
    {
        _context.Set<TModel>().Add(model);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetById", new { id = model.ID }, model);
    }

    // PUT: api/Controllers/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

    [HttpPut("{id}")]
    public async Task<IActionResult> PutController(int id, TModel model)
    {
        if (id != model.ID)
        {
            return BadRequest();
        }

        _context.Entry(model).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ModelExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Controllers/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<TModel>> DeleteCustomer(int id)
    {
        var controller = await _context.Set<TModel>().FindAsync(id);
        if (controller == null)
        {
            return NotFound();
        }

        _context.Entry(controller).State = EntityState.Deleted;
        await _context.SaveChangesAsync();

        return controller;
    }

    private bool ModelExists(int id)
    {
        return _context.Set<TModel>().Any(e => e.ID == id);
    }
    }
}
