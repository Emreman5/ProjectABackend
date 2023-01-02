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
    internal class OrderDetailManager : IOrderDetail
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderDetailRepository _orderDetail;

        public OrderDetailManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _orderDetail = _unitOfWork.OrderDetailRepository;
        }

        public IDataResult<List<OrderDetail>> GetAll()
        {
            var result = _orderDetail.Query().ToList();
            return new SuccessDataResult<List<OrderDetail>>(result);
        }

        public IResult Add(OrderDetail orderDetail)
        {
            _orderDetail.AddAsync(orderDetail);
            return new SuccessResult();
        }

        public IResult Update(OrderDetail orderDetail)
        {
            _orderDetail.UpdateAsync(orderDetail);
            return new SuccessResult();
        }

        public IDataResult<OrderDetail> GetById(int id)
        {
            var result = _orderDetail.GetByIdAsync(id).Result;
            return new SuccessDataResult<OrderDetail>(result);
        }

        public IResult Delete(int id)
        {
            var selected = _orderDetail.GetByIdAsync(id).Result;
            _orderDetail.DeleteAsync(selected);
            return new SuccessResult();
        }
    }
}
