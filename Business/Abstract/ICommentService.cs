using Core.Utilities.ResponseTypes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICommentService
    {
        IDataResult<List<MenuComment>> GetAll();
        IResult Add(MenuComment comment);
        IResult Update(MenuComment comment);
        IDataResult<MenuComment> GetById(int id);
        IResult Delete(int id);
    }
}
