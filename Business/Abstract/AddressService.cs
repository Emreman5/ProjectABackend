using Core.Utilities.ResponseTypes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface AddressService
    {
        IDataResult<List<Adress>> GetAll();
        IResult Add(Adress address);
        IResult Update(Adress address);
        IDataResult<Adress> GetById(int id);
        IResult Delete(int id);
    }
}
