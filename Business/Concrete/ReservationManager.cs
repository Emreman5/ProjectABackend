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
    internal class ReservationManager : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReservationRepository _reservation;
        public ReservationManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _reservation = _unitOfWork.ReservationRepository;
        }

        public IDataResult<List<Reservation>> GetAll()
        {
            var result = _reservation.Query().ToList();
            return new SuccessDataResult<List<Reservation>>(result);
        }

        public IResult Add(Reservation reservation)
        {
            _reservation.AddAsync(reservation);
            return new SuccessResult();
        }

        public IResult Update(Reservation reservation)
        {
            _reservation.UpdateAsync(reservation);
            return new SuccessResult();
        }

        public IDataResult<Reservation> GetById(int id)
        {
            var selected = _reservation.GetByIdAsync(id).Result;
            return new SuccessDataResult<Reservation>(selected);
        }

        public IResult Delete(int id)
        {
            var selected = _reservation.GetByIdAsync(id).Result;
            _reservation.DeleteAsync(selected);
            return new SuccessResult();
        }
    }
}
