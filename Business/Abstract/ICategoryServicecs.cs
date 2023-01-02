using Core.Utilities.ResponseTypes;
using Model;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll();
        Task<IResult> Add(Category category);
        IResult Update(Category category);
        IDataResult<Category> GetById(int id);
        IResult Delete(int id);
    }
}
