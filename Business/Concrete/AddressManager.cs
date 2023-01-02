using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.ResponseTypes;
using DataAccess.Concrete.Repositories.Abstract;
using DataAccess.Concrete.Repositories.Concrete;
using DataAccess.Concrete.UnitOfWork;
using Model;

namespace Business.Concrete
{
    public class AddressManager : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressRepository _address;
        public AddressManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _address = _unitOfWork.AdressRepository;
        }

        public IDataResult<List<Adress>> GetAll()
        {
            var result = _address.Query().ToList();
            return new SuccessDataResult<List<Adress>>(result);
        }

        public IResult Add(Adress address)
        {
            _address.AddAsync(address);
            return new SuccessResult();
        }

        public IResult Update(Adress address)
        {
           _address.UpdateAsync(address);
           return new SuccessResult();
        }

        public IDataResult<Adress> GetById(int id)
        {
            var result = _address.GetByIdAsync(id).Result;
            return new SuccessDataResult<Adress>(result);
        }

        public IResult Delete(int id)
        {
            var selectedAddress = _address.GetByIdAsync(id).Result;
            _address.DeleteAsync(selectedAddress);
            return new SuccessResult();
        }
    }
}
