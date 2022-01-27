using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Routing;

public class PaginatedList<TPagin>
{
    private int _pageIndex;
    public int TotalPages { get; set; }
    //const int maxPageSize = 3; 
    private int _pageSize;
    private readonly IQueryable<TPagin> _items;


    public PaginatedList(IQueryable<TPagin> items, int pageIndex, int pageSize)
    {
        _pageIndex = pageIndex;
        _pageSize = pageSize;
        _items = items;
    }

    public bool HasPreviousPage
    {
        get
        {
            return (_pageIndex > 1);
        }
    }

    public bool HasNextPage
    {
        get
        {
            return (_pageIndex < TotalPages);
        }
    }


    public int PageSize
    {
        get
        {
            return _pageSize;
        }
    }

    public async Task<IQueryable<TPagin>> PagineAsync()
    {
        var count = await _items.CountAsync();
        TotalPages = (int)Math.Ceiling(count / (double)_pageSize);
        return _items.Skip((_pageIndex - 1) * _pageSize)
                     .Take(_pageSize);
    }
}