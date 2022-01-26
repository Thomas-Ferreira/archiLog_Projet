using Archi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Archi.api.Extensions
{

    public static class QueryExtensions
    {
        public static void Sort<TModel>(this IQueryable<TModel> query, Settings setting)
        {
            if (setting.HasOrder())
            {
                string champ = setting.Asc;
                query = query.OrderBy(x => x.GetType().GetProperty(champ, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance));
            }
        }
    }
}