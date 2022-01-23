using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PaginatedList<T>
{
    private int _pageIndex;
    public int TotalPages { get; set; }
    const int maxPageSize = 2; 
    private int _pageSize;
    private readonly IQueryable<T> _items;
  

    public PaginatedList(IQueryable<T> items, int pageIndex, int pageSize)
    {
        _pageIndex = pageIndex;
        _pageSize = pageSize;
        _items = items;
    }
    public bool HasPreviousPage
    {
        get
        {
            return ( _pageIndex > 1);
        }
    }

    public bool HasNextPage
    {
        get
        {
            return ( _pageIndex < TotalPages);
        }
    }
    
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = ( _pageIndex > maxPageSize) ? maxPageSize : _pageIndex;
        }
    }

    public async Task<IQueryable<T>> PagineAsync()
    {
        var count = await _items.CountAsync();
        TotalPages = (int)Math.Ceiling(count / (double)_pageSize);
        return _items.Skip((_pageIndex - 1) * _pageSize)
                     .Take(_pageSize);
    }
}

