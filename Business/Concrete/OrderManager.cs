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
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = _unitOfWork.Order;
        }

        public IDataResult<List<Order>> GetAll()
        {
            var result = _orderRepository.Query().ToList();
            return new SuccessDataResult<List<Order>>(result);
        }

        public IResult Add(Order order)
        {
            _orderRepository.AddAsync(order);
            return new SuccessResult();
        }

        public IResult Update(Order order)
        {
            _orderRepository.UpdateAsync(order);
            return new SuccessResult();
        }

        public IDataResult<Order> GetById(int id)
        {
            var selected = _orderRepository.GetByIdAsync(id).Result;
            return new SuccessDataResult<Order>(selected);
        }

        public IResult Delete(int id)
        {
            var selected = _orderRepository.GetByIdAsync(id).Result;
            _orderRepository.DeleteAsync(selected);
            return new SuccessResult();
        }
    }
}
