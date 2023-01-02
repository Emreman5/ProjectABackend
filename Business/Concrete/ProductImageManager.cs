using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.ResponseTypes;
using DataAccess.Concrete.Repositories.Abstract;
using DataAccess.Concrete.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Model;
using Model.DTO;

namespace Business.Concrete
{
    public class ProductImageManager : IProductImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuImageRepository _image;
        private readonly IFileHelper _fileHelper;

        public ProductImageManager(IUnitOfWork unitOfWork, IFileHelper fileHelper)
        {
            _unitOfWork = unitOfWork;
            _fileHelper = fileHelper;
            _image = _unitOfWork.MenuImage;
        }

        public async Task<IResult> Add(ProductImageDto dto)
        {
            var result = new MenuImage() { MenuId = dto.ProductId };
            result.ImagePath = _fileHelper.Upload(dto.File, PathConstants.ImagesPath);
            result.Date = DateTime.Now;
            await _image.AddAsync(result);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult();
        }

        public IResult Delete(MenuImage productImage)
        {
            _fileHelper.Delete(PathConstants.ImagesPath + productImage.ImagePath);
            _image.DeleteAsync(productImage);
            return new SuccessResult();
        }

        public IResult Update(IFormFile file, MenuImage productImage)
        {
            productImage.ImagePath = _fileHelper.Update(file, PathConstants.ImagesPath + productImage.ImagePath, PathConstants.ImagesPath);
            _image.DeleteAsync(productImage);
            return new SuccessResult();
        }

        public IDataResult<List<MenuImage>> GetAll()
        {
            var result = _image.Query().ToList();
            return new SuccessDataResult<List<MenuImage>>(result);

        }

        public IDataResult<List<MenuImage>> GetById(int prodcutId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<byte[]> GetByImageId(int imageId)
        {
            var selectedImage = _image.GetByIdAsync(imageId).Result;
            Byte[] b = System.IO.File.ReadAllBytes(selectedImage.ImagePath);
            return new SuccessDataResult<byte[]>(b);
        }
    }
}
