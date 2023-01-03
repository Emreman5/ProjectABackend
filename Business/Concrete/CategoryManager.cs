using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.ResponseTypes;
using DataAccess.Concrete.Repositories.Abstract;
using DataAccess.Concrete.UnitOfWork;
using Model;
using Model.DTO;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _category;


        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _category = _unitOfWork.CategoryRepository;
        }

        public IDataResult<List<Category>> GetAll()
        {
            var result = _category.Query().ToList();
            return new SuccessDataResult<List<Category>>(result);
        }

        public  async Task<IResult> Add(CategoryDto category)
        {
            await _category.AddAsync(category.CreateEntity());
            await _unitOfWork.CompleteAsync();
            return new SuccessResult();
        }

        public IResult Update(CategoryDto category, int id)
        {
            var model = category.CreateEntity();
            model.Id = id;
            _category.UpdateAsync(model);
            _unitOfWork.CompleteAsync();
            return new SuccessResult();
        }

        public IDataResult<Category> GetById(int id)
        {
            var selectedCategory = _category.GetByIdAsync(id).Result;
            return new SuccessDataResult<Category>(selectedCategory);
        }

        public IResult Delete(int id)
        {
            var selectedCategory = _category.GetByIdAsync(id).Result;
            _category.DeleteAsync(selectedCategory);
            _unitOfWork.CompleteAsync();
            return new SuccessResult();
        }
    }
}
