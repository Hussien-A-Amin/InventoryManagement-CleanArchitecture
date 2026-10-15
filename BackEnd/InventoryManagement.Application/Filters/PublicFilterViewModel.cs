using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Filters;

public class BaseFilterViewModel
{
    int page = 1;

    public int Page { get => page; set { page = value > 1 ? value : 1; } }


    int pageSize = 20;
    public int PageSize { get => PageSizeOptions.Contains(pageSize.ToString()) ? pageSize : 20; set { pageSize = value; } }

    
    public int ItemsCounter { get; internal set; }

     List<string> PageSizeOptions => ["20", "30", "100"];


}
public class PublicFilterViewModel: BaseFilterViewModel
{
}
