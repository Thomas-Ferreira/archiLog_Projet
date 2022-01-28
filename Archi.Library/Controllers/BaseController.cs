﻿using Archi.Library.Data;
using Archi.Library.Models;
using Microsoft.AspNetCore.Http;
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
            var result = Sort(query, param, Request.QueryString);

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

        protected IOrderedQueryable<TModel> Sort(IQueryable<TModel> query, Params param, QueryString queryString)
        {
            if (param.HasOrderby())
            {
                string champAsc = param.Asc;
                string champDesc = param.Desc;
                string[] queryOrderbyAsc = champAsc != null ? champAsc?.ToString().Split(',') : new string[0];
                string[] queryOrderbyDesc = champDesc != null ? champDesc.ToString().Split(',') : new string[0];

                if (param.isAsc(queryString) == true && champAsc != null)
                {
                    var lambda = CreateLambda<TModel>(queryOrderbyAsc.FirstOrDefault());
                    var resultquery = query.OrderBy(lambda);

                        foreach (string element in queryOrderbyAsc.Skip(1))
                        {
                            if (element != null)
                            {
                                var lambda2 = CreateLambda<TModel>(element);
                                resultquery = resultquery.ThenBy(lambda2);
                            }
                        }

                    if (champDesc == null)
                    {
                        return resultquery;
                    }
                    else
                    {
                        foreach (string element in queryOrderbyDesc)
                        {
                            if (element != null)
                            {
                                var lambda2 = CreateLambda<TModel>(element);
                                resultquery = resultquery.ThenByDescending(lambda2);
                            }
                        }

                        return resultquery;
                    }
                }
                else /*if (param.isAsc(queryString) == false)*/
                {
                    var lambda = CreateLambda<TModel>(queryOrderbyDesc.FirstOrDefault());
                    var resultquery = query.OrderByDescending(lambda);

                    foreach (string element in queryOrderbyDesc.Skip(1))
                    {
                        if (element != null)
                        {
                            var lambda2 = CreateLambda<TModel>(element);
                            resultquery = resultquery.ThenByDescending(lambda2);
                        }
                    }

                    if (champAsc == null)
                    {
                        return resultquery;
                    }
                    else
                    {
                        foreach (string element in queryOrderbyAsc)
                        {if (element != null)
                            {
                                var lambda2 = CreateLambda<TModel>(element);
                                resultquery = resultquery.ThenBy(lambda2);
                            }
                        }

                        return resultquery;
                    }
                }
            }
            return (IOrderedQueryable<TModel>)query;
        }

        private Expression<Func<TModel, object>> CreateLambda<Model>(string champ)
        {
            var parameter = Expression.Parameter(typeof(Model), "x");
            var property = Expression.Property(parameter, champ);
            var o = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda<Func<TModel, object>>(o, parameter);
            return lambda;
        }

    }
}
