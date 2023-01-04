using Core.Utilities.ResponseTypes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Pagination;
using Microsoft.AspNetCore.Http;
using Model.Abstract;
using Model.DTO;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll(PaginationFilter filter, string route);
         Task<IResult> Add(ProductPostDto product, List<IFormFile> files);
        IResult Update(ProductPostDto product, int id);
        IDataResult<Product> GetById(int id);
        Task<IResult> Delete(int id);
        Task<IDataResult<ProductDetailDto>> GetProductDetailById(int id, string route);
        Task<IDataResult<List<ProductDetailDto>>> GetAllWithDetails(PaginationFilter filter, string route);
        Task<IDataResult<List<ProductDetailDto>>> GetByCategoryId(PaginationFilter filter, string route, int categoryId);



    }
}
