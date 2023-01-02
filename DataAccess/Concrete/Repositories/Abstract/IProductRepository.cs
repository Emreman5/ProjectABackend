using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.Repository;
using Core.Utilities.Pagination;
using Core.Utilities.Uri;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Abstract;

namespace DataAccess.Concrete.Repositories.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ProductDetailDto> GetProductDetailDtoById(int id, string route, IUriService uriService);
        Task<List<ProductDetailDto>> GetProductDetails(string route, IUriService uriService);

    }
}
