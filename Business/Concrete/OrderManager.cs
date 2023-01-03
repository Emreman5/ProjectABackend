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
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = _unitOfWork.Order;
            _orderDetailRepository = _unitOfWork.OrderDetailRepository;
        }


        public async Task<IResult> CreateOrder(OrderDto order)
        {
            var ordersDetailsDto = order.Orders;
            var orderEntity = order.CreateEntity();
            await _orderRepository.AddAsync(orderEntity);
            await _unitOfWork.CompleteAsync();
            int id = _orderRepository.FindAllAsync(o => o.Id == order.CustomerId).Result.Last().Id;
            var ordersDetails = ordersDetailsDto.Select(o => o.CreateEntity(id));
            await _orderDetailRepository.AddRange(ordersDetails);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult();
        }

        public IResult DeleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<Order>>> GetOrdersByCustomerId(int customerId)
        {
            var orders = await _orderRepository.FindAllAsync(o => o.CustomerId == customerId);
            return new SuccessDataResult<List<Order>>(orders.ToList());
        }

        public async Task<IDataResult<List<Order>>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            return new SuccessDataResult<List<Order>>(orders.ToList());
        }

        public async Task<IDataResult<List<OrderDetail>>> GetOrderDetailsById(int orderId)
        {
            var order = await _orderDetailRepository.FindAllAsync(o => o.OrderId == orderId);
            return new SuccessDataResult<List<OrderDetail>>(order.ToList());
        }
    }
}
