using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Helpers;
using Core.Utilities.Pagination;
using Core.Utilities.ResponseTypes;
using Core.Utilities.Uri;
using DataAccess.Concrete.Repositories.Abstract;
using DataAccess.Concrete.UnitOfWork;
using Model;
using Model.Abstract;
using Model.DTO;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly  IUnitOfWork _unitOfWork;
        private readonly IProductRepository _product;
        private readonly IUriService _uriService;
        public ProductManager(IUnitOfWork unitOfWork, IUriService uriService)
        {
            _unitOfWork = unitOfWork; 
            _uriService = uriService;
            _product = _unitOfWork.ProductRepository;
        }

        public IDataResult<List<Product>> GetAll(PaginationFilter filter, string route)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = _product.GetPagedData(filter).Result;
            var totalRecords = _product.GetTotalRecords().Result;
            var pagedResult =
                PaginationHelper.CreatePagedResponse<Product>(pagedData, validFilter, totalRecords, _uriService, route);
            return pagedResult;
        }

        public IResult Add(ProductPostDto product)
        {
            _product.AddAsync(product.CreateEntity());
            _unitOfWork.CompleteAsync();
            return new SuccessResult();
        }

        public IResult Update(ProductPostDto product, int id)
        {
            var param = product.CreateEntity();
            param.Id = id;
            _product.UpdateAsync(param);
            return new SuccessResult();
        }

        public IDataResult<Product> GetById(int id)
        {
           var selected = _product.GetByIdAsync(id).Result;
           return new SuccessDataResult<Product>(selected);
        }

        public IResult Delete(int id)
        {
           var selected = _product.GetByIdAsync(id).Result;
           _product.DeleteAsync(selected);
           _unitOfWork.CompleteAsync();
           return new SuccessResult();
        }

        public async Task<IDataResult<ProductDetailDto>> GetProductDetailById(int id, string route)
        {
            var result =  await _product.GetProductDetailDtoById(id, route, _uriService);
            return new SuccessDataResult<ProductDetailDto>(result);
        }

        public async Task<IDataResult<List<ProductDetailDto>>> GetAllWithDetails(PaginationFilter filter, string route)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData =  await _product.GetProductDetails(route, _uriService);
            var totalRecords = await _product.GetTotalRecords();
            var pagedResult =
                PaginationHelper.CreatePagedResponse<ProductDetailDto>(pagedData, validFilter, totalRecords, _uriService, route);
            return await Task.FromResult<IDataResult<List<ProductDetailDto>>>(pagedResult);
        }
    }
}
