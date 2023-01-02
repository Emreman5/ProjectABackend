using Core.Utilities.ResponseTypes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DTO;

namespace Business.Abstract
{
    public interface IProductImageService
    {
        Task<IResult> Add(ProductImageDto image);
        IResult Delete(MenuImage carImage);
        IResult Update(IFormFile file, MenuImage carImage);

        IDataResult<List<MenuImage>> GetAll();
        IDataResult<List<MenuImage>> GetById(int id);
        IDataResult<byte[]> GetByImageId(int imageId);
    }
}
