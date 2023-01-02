using Core.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace Core.Utilities.Uri
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public System.Uri GetPageUri(PaginationFilter filter, string route)
        {
            var _enpointUri = new System.Uri(string.Concat(_baseUri, route));
            var modifiedUri =
                QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new System.Uri(modifiedUri);
        }

        public System.Uri GetImageUri(string route, int imageId)
        {
            var _endpointUri = new System.Uri(string.Concat(_baseUri, route, imageId));
            var modifiedUri = _endpointUri;
            return new System.Uri(modifiedUri.ToString());
        }
    }
}
