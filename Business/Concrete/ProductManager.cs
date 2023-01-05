using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Pagination;
using Core.Utilities.ResponseTypes;
using Core.Utilities.Uri;
using DataAccess.Concrete.Repositories.Abstract;
using DataAccess.Concrete.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Model;
using Model.Abstract;
using Model.DTO;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly  IUnitOfWork _unitOfWork;
        private readonly IProductRepository _product;
        private readonly IProductImageService _imageService;
        private readonly IUriService _uriService;
        public ProductManager(IUnitOfWork unitOfWork, IUriService uriService, IProductImageService imageService)
        {
            _unitOfWork = unitOfWork; 
            _uriService = uriService;
            _product = _unitOfWork.ProductRepository;
            _imageService = imageService;
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

        public async Task<IResult> Add(ProductPostDto product, List<IFormFile> files)
        {
            var selectedCategory = _unitOfWork.CategoryRepository.Query().FirstOrDefault(c => c.CategoryName == product.Category);
            await _product.AddAsync(product.CreateEntity(selectedCategory.Id));
            await _unitOfWork.CompleteAsync();
            var products = await _product.GetAllAsync();
            var productId = products.Last().Id;
            foreach (var item in files)
            {
                _imageService.Add(new ProductImageDto(){ProductId = productId, File = item});
            }
            _unitOfWork.CompleteAsync();
            return new SuccessResult();
        }

        public async Task<IResult> Update(ProductPostDto product, int id)
        {
            var logic = BusinessRules.Run(await CheckIfProductExistsAsync(id), await CheckIfCategoryExistsByName(product.Category));
            if (!logic.IsSuccess)
            {
                return new ErrorDataResult<IResult>(logic.Message);
            }
            var selectedCategory = _unitOfWork.CategoryRepository.Query().FirstOrDefault(c => c.CategoryName == product.Category);
            var param = product.CreateEntity(selectedCategory.Id);
            param.Id = id;
            await _product.UpdateAsync(param);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult();
        }

        public IDataResult<Product> GetById(int id)
        {
            var logic = BusinessRules.Run( CheckIfProductExists(id));
            if (!logic.IsSuccess)
            {
                return new ErrorDataResult<Product>(logic.Message);
            }
            var selected = _product.GetByIdAsync(id).Result;
           return new SuccessDataResult<Product>(selected);
        }

        public async Task<IResult> Delete(int id)
        {
            var logic = BusinessRules.Run(await CheckIfProductExistsAsync(id));
            if (!logic.IsSuccess)
            {
                return new ErrorDataResult<ProductDetailDto>(logic.Message);
            }
            var selected = await _product.GetByIdAsync(id);
           await _product.DeleteAsync(selected);
           await _unitOfWork.CompleteAsync();
           return new SuccessResult();
        }

        public async Task<IDataResult<ProductDetailDto>> GetProductDetailById(int id, string route)
        {
            var logic = BusinessRules.Run(await CheckIfProductExistsAsync(id));
            if (!logic.IsSuccess)
            {
                return new ErrorDataResult<ProductDetailDto>(logic.Message);
            }
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

        public async Task<IDataResult<List<ProductDetailDto>>> GetByCategoryId(PaginationFilter filter, string route, int categoryId)
        {
            var logic = BusinessRules.Run(await CheckIfCategoryExists(categoryId));
            if (!logic.IsSuccess)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(logic.Message);
            }
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _product.GetProductDetailsByCategoryId(route, _uriService, categoryId);
            var totalRecords = await _product.GetTotalRecords();
            var pagedResult =
                PaginationHelper.CreatePagedResponse<ProductDetailDto>(pagedData, validFilter, totalRecords, _uriService, route);
            return await Task.FromResult<IDataResult<List<ProductDetailDto>>>(pagedResult);
        }


        private async Task<IResult> CheckIfProductExistsAsync(int id)
        {
            var product = await _product.GetByIdAsync(id);
            if (product == null)
            {
                return new ErrorResult("Bu Id'ye sahip product bulunamadı");
            }
            return new SuccessResult();

        }
        private IResult CheckIfProductExists(int id)
        {
            var product = _unitOfWork.GetContext().Menus.FirstOrDefault(m => m.Id == id);
            {
                return new ErrorResult("Bu Id'ye sahip product bulunamadı");
            }
            return new SuccessResult();

        }
        private async Task<IResult> CheckIfCategoryExists(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                return new ErrorResult("Bu Id'ye sahip Category bulunamadı");
            }
            return new SuccessResult();

        }
        private async Task<IResult> CheckIfCategoryExistsByName(string name)
        {
            var category = await _unitOfWork.CategoryRepository.FindAsync(c => c.CategoryName == name);
            if (category == null)
            {
                return new ErrorResult("Bu Id'ye sahip Category bulunamadı");
            }
            return new SuccessResult();

        }
    }
}
