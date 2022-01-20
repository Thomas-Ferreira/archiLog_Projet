using Archi.Library.Data;
using Archi.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Archi.Library.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController<Tcontext, TModel> : ControllerBase where Tcontext : BaseDbContext where TModel : BaseModel 
    {
        protected readonly Tcontext _context;
        public BaseController(Tcontext context)
        {
            _context = context;
        }

        // GET: api/[Controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TModel>>> GetAll([FromQuery]Params param)
        {
            var query = _context.Set<TModel>().Where(x => x.Active == true);
            var result = Sort(query, param);

            return await result.ToListAsync();

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

        // GET: api/[Controller]
        /*[HttpGet("sortby")]
        public async Task<ActionResult<IEnumerable<TModel>>> GetByOrder( string desc )
        {
            var controller = await _context.Set<TModel>().OrderByDescending(x => x.GetType().GetProperty(desc)).Where(x => x.Active == true).ToListAsync();
            return controller;
        }*/

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

        protected IOrderedQueryable<TModel> Sort(IQueryable<TModel> query, Params param)
        {
            if (param.HasOrderby())
            {
                string champAsc = param.asc;
                string champDesc = param.desc;
                
                var lambda = CreateLambda(champDesc);

                var lambda2 = CreateLambda(champAsc);

                return query.OrderByDescending(lambda).ThenBy(lambda2);
            }
            else
                return (IOrderedQueryable<TModel>)query;
        }

        private Expression<Func<TModel, object>> CreateLambda(string champ)
        {
            var parameter = Expression.Parameter(typeof(TModel), "x");
            var property = Expression.Property(parameter, champ);
            var o = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda<Func<TModel, object>>(o, parameter);
            return lambda;
        }

    }
}
