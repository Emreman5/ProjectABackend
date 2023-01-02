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

namespace Business.Concrete
{
    internal class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentRepository _comment;

        public CommentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _comment = _unitOfWork.CommentRepository;
        }

        public IDataResult<List<MenuComment>> GetAll()
        {
            var result = _comment.Query().ToList();
            return new SuccessDataResult<List<MenuComment>>(result);
        }

        public IResult Add(MenuComment comment)
        {
            _comment.AddAsync(comment);
            return new SuccessResult();
        }

        public IResult Update(MenuComment comment)
        {
            _comment.UpdateAsync(comment);
            return new SuccessResult();
        }

        public IDataResult<MenuComment> GetById(int id)
        {
            var result = _comment.GetByIdAsync(id).Result;
            return new SuccessDataResult<MenuComment>(result);
        }

        public IResult Delete(int id)
        {
            var selectedComment = _comment.GetByIdAsync(id).Result;
            _comment.DeleteAsync(selectedComment);
            return new SuccessResult();
        }
    }
}
