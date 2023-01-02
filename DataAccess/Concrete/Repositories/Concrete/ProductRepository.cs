using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.Repository;
using Core.Utilities.Pagination;
using Core.Utilities.Uri;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Abstract;
using Model.DTO;

namespace DataAccess.Concrete.Repositories.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly MsSqlDbContext _context;
        public ProductRepository(DbContext context) : base(context)
        {
            _context = (MsSqlDbContext) context;
        }

        public Task<ProductDetailDto> GetProductDetailDtoById(int id, string route, IUriService uriService)
        {
            var resultInformation = from product in _context.Menus
                join category in _context.Categories on product.CategoryId equals category.Id
                where product.Id == id
                select new { ProductName = product.Name, CategoryName = category.CategoryName, Price = product.Price, Description = product.Description, Id = product.Id };
            var information = resultInformation.First();
            var resultImages = _context.MenuImages.Where(p => p.MenuId == id).ToList();
            var imageUrls = new List<System.Uri>();
            foreach (var image in resultImages)
            {
                imageUrls.Add(uriService.GetImageUri(route, image.Id));
            }

            var result = new ProductDetailDto()
            {

                ProductName = information.ProductName,
                CategoryName = information.CategoryName,
                Price = information.Price,
                Description = information.Description,
                Images = imageUrls,
                Id = information.Id
            };
            return Task.FromResult(result);
        }

        public async Task<List<ProductDetailDto>> GetProductDetails(string route, IUriService uriService)
        {
            var products = _context.Menus.ToList();
            var result = products.Select(p => GetProductDetailDtoById(p.Id, route, uriService).Result).ToList();
            return result;
        }
        
    }
}
