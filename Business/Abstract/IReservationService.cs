using Core.Utilities.ResponseTypes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IReservationService
    {
        IDataResult<List<Reservation>> GetAll();
        IResult Add(Reservation reservation);
        IResult Update(Reservation reservation);
        IDataResult<Reservation> GetById(int id);
        IResult Delete(int id);
    }
}
