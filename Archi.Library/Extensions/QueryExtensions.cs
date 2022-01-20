using Archi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Archi.Library.Extensions
{
    public static class QueryExtensions
    {
        public static IOrderedQueryable<TModel> Sort<TModel>(this IQueryable<TModel> query, Filtre filtre)
        {
            if (filtre.HasOrder())
            {
                string champ = filtre.Asc;
                //var property = typeof(TModel).GetProperty(champ, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance);
                //query = query.OrderBy(x => property.GetValue(x));

                //Créer lambda
                var parameter = Expression.Parameter(typeof(TModel), "x");
                var property = Expression.Property(parameter, "Lastname");
                var o = Expression.Convert(property, typeof(object));
                var lambda = Expression.Lambda<Func<TModel, object>>(o, parameter);

                //Utiliser
                return query.OrderBy(lambda);

            }
            else
                return (IOrderedQueryable<TModel>)query;
        }
    }
}