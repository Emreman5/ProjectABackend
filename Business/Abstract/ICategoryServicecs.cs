using Core.Utilities.ResponseTypes;
using Model;
using Model.DTO;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll();
        Task<IResult> Add(CategoryDto category);
        IResult Update(CategoryDto category, int id);
        IDataResult<Category> GetById(int id);
        IResult Delete(int id);
    }
}
