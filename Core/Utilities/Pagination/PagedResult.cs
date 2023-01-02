using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.ResponseTypes;

namespace Core.Utilities.Pagination
{
    public class PagedResult<T> : DataResult<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public System.Uri FirstPage { get; set; }
        public System.Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public System.Uri NextPage { get; set; }
        public System.Uri PreviousPage { get; set; }

        public PagedResult(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.IsSuccess = true;
        }
    }
}
