using Core.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Uri
{
    public interface IUriService
    {
        public System.Uri GetPageUri(PaginationFilter filter, string route);
        System.Uri GetImageUri(string route, int imageId);
    }
}
